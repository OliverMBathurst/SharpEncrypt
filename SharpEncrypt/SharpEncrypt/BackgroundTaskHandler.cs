using System.Collections.Concurrent;
using System.ComponentModel;
using System;
using System.Threading;
using SharpEncrypt.AbstractClasses;
using System.Collections.Generic;

namespace SharpEncrypt
{
    public sealed class BackgroundTaskHandler : IDisposable
    {
        private readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly ConcurrentQueue<SharpEncryptTask> Tasks = new ConcurrentQueue<SharpEncryptTask>();

        public delegate void TaskDequeued(SharpEncryptTask task);
        public delegate void TaskCompleted(SharpEncryptTask task);
        public delegate void CurrentTasksCompleted();

        public event TaskCompleted TaskCompletedEvent;
        public event TaskDequeued TaskDequeuedEvent;
        public event CurrentTasksCompleted TasksCompleted;

        public BackgroundTaskHandler()
        {
            BackgroundWorker.DoWork += BackgroundWorkerWork;
            TaskCompletedEvent += BackgroundTaskHandler_TaskCompletedEvent;
        }

        public bool HasCompletedJobs => Tasks.IsEmpty && (CurrentTask.SharpEncryptTask.InnerTask == null || CurrentTask.SharpEncryptTask.InnerTask.IsCompleted);

        public int TaskCount => Tasks.Count;

        public bool IsBusy => BackgroundWorker.IsBusy;

        public (SharpEncryptTask SharpEncryptTask, CancellationTokenSource CancelToken) CurrentTask { get; private set; }

        public List<(SharpEncryptTask Task, DateTime Time)> CompletedTasks { get; } = new List<(SharpEncryptTask task, DateTime time)>();
        
        public void AddJob(SharpEncryptTask task, bool synchronous = false) 
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            Tasks.Enqueue(task);
            if (synchronous)
                task.InnerTask.Wait();
        }

        public void CancelWorker()
        {
            BackgroundWorker.CancelAsync();
        }

        public void CancelAllTasks()
        {
            BackgroundWorker.CancelAsync();
            
            while (!Tasks.IsEmpty)
                Tasks.TryDequeue(out _);

            if (CurrentTask.SharpEncryptTask.InnerTask != null)
                CurrentTask.CancelToken.Cancel();            
        }

        public void Run()
        {
            if (!BackgroundWorker.IsBusy && !BackgroundWorker.CancellationPending)
                BackgroundWorker.RunWorkerAsync();
        }

        #region Events
        private void BackgroundTaskHandler_TaskCompletedEvent(SharpEncryptTask task)
        {
            CompletedTasks.Add((task, DateTime.Now));
        }

        private void OnTaskCompleted(SharpEncryptTask task)
        {
            TaskCompletedEvent?.Invoke(task);
        }

        private void OnTaskDequeued(SharpEncryptTask task)
        {
            TaskDequeuedEvent?.Invoke(task);
        }

        private void OnCurrentTasksCompleted()
        {
            TasksCompleted?.Invoke();
        }

        public void Dispose()
        {
            BackgroundWorker.Dispose();
        }

        #endregion

        private void BackgroundWorkerWork(object sender, DoWorkEventArgs e)
        {
            while (!BackgroundWorker.CancellationPending)
            {
                if (Tasks.TryDequeue(out var task))
                {
                    OnTaskDequeued(task);
                    var cancellationTokenSource = new CancellationTokenSource();
                    CurrentTask = (task, cancellationTokenSource);
                    CurrentTask.SharpEncryptTask.InnerTask.Start();
                    CurrentTask.SharpEncryptTask.InnerTask.Wait(cancellationTokenSource.Token);
                    OnTaskCompleted(CurrentTask.SharpEncryptTask);

                    if (Tasks.IsEmpty)
                        OnCurrentTasksCompleted();
                }
            }
        }
    }
}