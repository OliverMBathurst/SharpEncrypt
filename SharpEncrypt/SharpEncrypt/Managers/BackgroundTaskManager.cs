using System.Collections.Concurrent;
using System.ComponentModel;
using System;
using System.Threading;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Exceptions;
using System.Collections.Generic;
using System.Linq;
using SharpEncrypt.Models;

namespace SharpEncrypt.Managers
{
    public sealed class BackgroundTaskManager : IDisposable
    {        
        private readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly ConcurrentQueue<SharpEncryptTask> Tasks = new ConcurrentQueue<SharpEncryptTask>();

        #region Delegates and events
        public delegate void BackgroundWorkerDisabledEventHandler(Guid guid);
        internal delegate void TaskDequeuedEventHandler(SharpEncryptTask task);
        internal delegate void TaskCompletedEventHandler(SharpEncryptTask task);
        public delegate void ExceptionOccurredEventHandler(Exception exception);
        public delegate void CurrentTasksCompletedEventHandler();

        internal event TaskCompletedEventHandler TaskCompleted;
        internal event TaskDequeuedEventHandler TaskDequeued;
        public event CurrentTasksCompletedEventHandler TasksCompleted;
        public event BackgroundWorkerDisabledEventHandler BackgroundWorkerDisabled;
        public event ExceptionOccurredEventHandler Exception;
        #endregion

        public BackgroundTaskManager(bool disableAfterTaskCompleted = true)
        {
            DisableAfterTaskCompleted = disableAfterTaskCompleted;

            BackgroundWorker.DoWork += BackgroundWorkerWork;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        #region Properties
        public Guid Identifier { get; } = Guid.NewGuid();

        public bool DisableAfterTaskCompleted { get; }

        public bool HasCompletedTasks => Tasks.IsEmpty && (CurrentTaskInstanceModel == null || CurrentTaskInstanceModel.Task.InnerTask.IsCompleted);

        public int TaskCount => Tasks.Count + (IsProcessingTask ? 1 : 0);

        public bool IsProcessingTask { get; private set; }

        public bool Disabled { get; private set; }

        internal CurrentTaskInstanceModel CurrentTaskInstanceModel { get; private set; }

        internal IEnumerable<SharpEncryptTask> ActiveTasks
        {
            get
            {
                var tasks = Tasks.ToList();
                if (CurrentTaskInstanceModel?.Task != null && !CurrentTaskInstanceModel.Task.InnerTask.IsCompleted)
                    tasks.Add(CurrentTaskInstanceModel.Task);
                return tasks;
            }
        }
        #endregion

        #region Other methods

        internal void AddTask(SharpEncryptTask task) 
        {
            if (Disabled)
            {
                Exception?.Invoke(new BackgroundTaskHandlerDisabledException());
                return;
            }

            if (task == null)
            {
                Exception?.Invoke(new ArgumentNullException(nameof(task)));
                return;
            }

            Tasks.Enqueue(task);
            if (!BackgroundWorker.IsBusy)
                BackgroundWorker.RunWorkerAsync();
        }

        public void CancelWorker() => BackgroundWorker.CancelAsync();

        public void CancelAllTasks()
        {
            BackgroundWorker.CancelAsync();
            
            while (!Tasks.IsEmpty)
                Tasks.TryDequeue(out _);

            if (CurrentTaskInstanceModel.Task.InnerTask != null)
                CurrentTaskInstanceModel.Source.Cancel();            
        }               

        public void Dispose() => BackgroundWorker.Dispose();

        private void BackgroundWorkerWork(object sender, DoWorkEventArgs e)
        {
            while (!BackgroundWorker.CancellationPending && !Tasks.IsEmpty && !Disabled)
            {
                if (!Tasks.TryDequeue(out var task)) continue;

                IsProcessingTask = true;
                OnTaskDequeued(task);
                CurrentTaskInstanceModel = new CurrentTaskInstanceModel { Task = task, Source = new CancellationTokenSource() };

                try
                {
                    task.Start();
                    task.Wait(CurrentTaskInstanceModel.Source.Token);
                }
                catch (Exception exception)
                {
                    task.Result.Exception = exception;
                }

                IsProcessingTask = false;
                OnTaskCompleted(task);

                if (Tasks.IsEmpty)
                    OnCurrentTasksCompleted();
            }
        }

        #endregion

        #region Events
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Tasks.IsEmpty && !Disabled)
                BackgroundWorker.RunWorkerAsync();
        }

        private void OnTaskCompleted(SharpEncryptTask task)
        {
            TaskCompleted?.Invoke(task);
            if (!DisableAfterTaskCompleted) return;
            Disabled = true;
            BackgroundWorkerDisabled?.Invoke(Identifier);
        }

        private void OnTaskDequeued(SharpEncryptTask task) => TaskDequeued?.Invoke(task);

        private void OnCurrentTasksCompleted() => TasksCompleted?.Invoke();

        #endregion
    }
}