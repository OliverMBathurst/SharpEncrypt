using System.Collections.Concurrent;
using System.ComponentModel;
using System;
using System.Threading;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SharpEncrypt
{
    public sealed class BackgroundTaskHandler : IDisposable
    {        
        private readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly ConcurrentQueue<SharpEncryptTask> mTasks = new ConcurrentQueue<SharpEncryptTask>();

        #region Delegates and events
        public delegate void BackgroundWorkerDisabledEventHandler(Guid guid);
        public delegate void TaskDequeuedEventHandler(SharpEncryptTask task);
        public delegate void TaskCompletedEventHandler(SharpEncryptTask task);
        public delegate void ExceptionOccurredEventHandler(Exception exception);
        public delegate void CurrentTasksCompletedEventHandler();

        public event TaskCompletedEventHandler TaskCompleted;
        public event TaskDequeuedEventHandler TaskDequeued;
        public event CurrentTasksCompletedEventHandler TasksCompleted;
        public event BackgroundWorkerDisabledEventHandler BackgroundWorkerDisabled;
        public event ExceptionOccurredEventHandler ExceptionOccurred;
        #endregion

        public BackgroundTaskHandler(bool disableAfterJob = true)
        {
            DisableAfterJob = disableAfterJob;

            BackgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerWork);
            BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
        }

        #region Properties
        public Guid Identifier { get; } = Guid.NewGuid();

        public bool DisableAfterJob { get; private set; }

        public bool HasCompletedJobs => mTasks.IsEmpty && (Current.Task.InnerTask == null || Current.Task.InnerTask.IsCompleted);

        public int TaskCount => mTasks.Count + (IsProcessingTask ? 1 : 0);

        public bool IsProcessingTask { get; private set; } = false;

        public bool Disabled { get; private set; } = false;

        public (SharpEncryptTask Task, CancellationTokenSource CancelToken) Current { get; private set; }

        public IEnumerable<SharpEncryptTask> Tasks
        {
            get
            {
                var tasks = mTasks.ToList();
                if (Current.Task != null && !Current.Task.InnerTask.IsCompleted)
                    tasks.Add(Current.Task);
                return tasks;
            }
        }
        #endregion

        #region Other methods

        public void AddTask(SharpEncryptTask task) 
        {
            if (Disabled)
            {
                ExceptionOccurred?.Invoke(new BackgroundTaskHandlerDisabledException());
                return;
            }

            if (task == null)
            {
                ExceptionOccurred?.Invoke(new ArgumentNullException(nameof(task)));
                return;
            }

            mTasks.Enqueue(task);
            if (!BackgroundWorker.IsBusy)
                BackgroundWorker.RunWorkerAsync();
        }

        public void CancelWorker()
        {
            BackgroundWorker.CancelAsync();
        }

        public void CancelAllTasks()
        {
            BackgroundWorker.CancelAsync();
            
            while (!mTasks.IsEmpty)
                mTasks.TryDequeue(out _);

            if (Current.Task.InnerTask != null)
                Current.CancelToken.Cancel();            
        }               

        public void Dispose()
        {
            BackgroundWorker.Dispose();
        }

        private void BackgroundWorkerWork(object sender, DoWorkEventArgs e)
        {
            while (!BackgroundWorker.CancellationPending && !mTasks.IsEmpty && !Disabled)
            {
                if (mTasks.TryDequeue(out var task))
                {
                    IsProcessingTask = true;
                    OnTaskDequeued(task);
                    var cancellationTokenSource = new CancellationTokenSource();
                    Current = (task, cancellationTokenSource);

                    try
                    {
                        task.Start();
                        task.Wait(cancellationTokenSource.Token);
                    }
                    catch (Exception exception)
                    {
                        task.Result.Exception = exception;
                    }

                    IsProcessingTask = false;
                    OnTaskCompleted(task);

                    if (mTasks.IsEmpty)
                        OnCurrentTasksCompleted();
                }
            }
        }

        #endregion

        #region Events
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!mTasks.IsEmpty && !Disabled)
                BackgroundWorker.RunWorkerAsync();
        }

        private void OnTaskCompleted(SharpEncryptTask task)
        {
            TaskCompleted?.Invoke(task);
            if (DisableAfterJob)
            {
                Disabled = true;
                BackgroundWorkerDisabled?.Invoke(Identifier);
            }
        }

        private void OnTaskDequeued(SharpEncryptTask task)
        {
            TaskDequeued?.Invoke(task);
        }

        private void OnCurrentTasksCompleted()
        {
            TasksCompleted?.Invoke();
        }

        #endregion
    }
}