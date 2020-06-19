using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;
using System;

namespace SharpEncrypt
{
    internal sealed class BackgroundTaskHandler : IDisposable
    {
        private readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly ConcurrentQueue<Task> Tasks = new ConcurrentQueue<Task>();

        public bool IsRunning { get; private set; } = false;

        public bool HasCompletedJobs { get; private set; } = true;

        public BackgroundTaskHandler() => BackgroundWorker.DoWork += BackgroundWorkerWork;        

        public void AddJob(Task task, bool synchronous = false) 
        {
            Tasks.Enqueue(task);
            if (synchronous)
                task.Wait();
        }

        public void Run()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                BackgroundWorker.RunWorkerAsync();
            }
        }

        private void BackgroundWorkerWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!Tasks.IsEmpty)
                {
                    HasCompletedJobs = false;
                    if (Tasks.TryDequeue(out var task))
                    {
                        task.Start();
                        task.Wait();
                    }
                }
                else
                {
                    HasCompletedJobs = true;
                }
            }
        }

        public void Dispose()
        {
            BackgroundWorker.Dispose();
        }
    }
}
