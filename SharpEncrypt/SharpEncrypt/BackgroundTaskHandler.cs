using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace SharpEncrypt
{
    public sealed class BackgroundTaskHandler : IDisposable
    {
        private readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly ConcurrentQueue<Task> Tasks = new ConcurrentQueue<Task>();

        public delegate void TaskDequeued(Task task);
        public delegate void TaskCompleted(Task task);
        public delegate void CurrentTasksCompleted();

        public event TaskCompleted TaskCompletedEvent;
        public event TaskDequeued TaskDequeuedEvent;
        public event CurrentTasksCompleted TasksCompleted;

        public bool HasCompletedJobs => Tasks.IsEmpty && (CurrentTask.Task == null || CurrentTask.Task.IsCompleted);

        public int TaskCount => Tasks.Count;

        public bool IsBusy => BackgroundWorker.IsBusy;

        public (Task Task, CancellationTokenSource CancelToken) CurrentTask { get; private set; }

        public BackgroundTaskHandler() => BackgroundWorker.DoWork += BackgroundWorkerWork;        

        public void AddJob(Task task, bool synchronous = false) 
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            Tasks.Enqueue(task);
            if (synchronous)
                task.Wait();
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

            if (CurrentTask.Task != null)
                CurrentTask.CancelToken.Cancel();            
        }

        public void Run()
        {
            if (!BackgroundWorker.IsBusy && !BackgroundWorker.CancellationPending)
                BackgroundWorker.RunWorkerAsync();
        }

        #region Events
        private void OnTaskCompleted(Task task)
        {
            TaskCompletedEvent?.Invoke(task);
        }

        private void OnTaskDequeued(Task task)
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
                    CurrentTask.Task.Start();
                    CurrentTask.Task.Wait(cancellationTokenSource.Token);
                    OnTaskCompleted(CurrentTask.Task);

                    if (Tasks.IsEmpty)
                        OnCurrentTasksCompleted();
                }
            }
        }
    }
}