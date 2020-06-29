using SharpEncrypt.AbstractClasses;
using System;
using System.Linq;
using System.Collections.Concurrent;

namespace SharpEncrypt
{
    public sealed class TaskManager : IDisposable
    {
        private readonly BackgroundTaskHandler ShortTaskHandler = new BackgroundTaskHandler(false);
        private readonly ConcurrentDictionary<Guid, BackgroundTaskHandler> TaskHandlers = new ConcurrentDictionary<Guid, BackgroundTaskHandler>();

        #region Delegates and events
        public delegate void ShortTaskCompletedEvent(SharpEncryptTask task);
        public delegate void LongTaskCompletedEvent(SharpEncryptTask task);
        public delegate void TaskDequeuedEvent(SharpEncryptTask task);
        public delegate void TaskManagerCompletedEvent();

        public event ShortTaskCompletedEvent ShortTaskCompleted;
        public event LongTaskCompletedEvent LongTaskCompleted;
        public event TaskDequeuedEvent TaskDequeued;
        public event TaskManagerCompletedEvent TaskManagerCompleted;
        #endregion

        public TaskManager()
        {
            ShortTaskHandler.TaskCompleted += ShortTaskHandler_TaskCompletedEvent;
            ShortTaskHandler.TaskDequeued += GenericTaskDequeued;
        }

        public bool Cancelled { get; private set; } = false;

        public ConcurrentBag<(SharpEncryptTask Task, DateTime Time)> CompletedTasks { get; } = new ConcurrentBag<(SharpEncryptTask task, DateTime time)>();

        public bool HasCompletedJobs => ShortTaskHandler.HasCompletedJobs && TaskHandlers.All(x => x.Value.HasCompletedJobs);

        public int TaskCount => ShortTaskHandler.TaskCount + TaskHandlers.Count;

        public void SetCancellationFlag() => Cancelled = true;
        
        #region Event methods

        private void GenericTaskDequeued(SharpEncryptTask task)
        {
            TaskDequeued?.Invoke(task);
        }

        private void ShortTaskHandler_TaskCompletedEvent(SharpEncryptTask task)
        {
            ShortTaskCompleted?.Invoke(task);
            AfterTaskCompleted(task);
        }

        private void LongTaskHandler_TaskCompletedEvent(SharpEncryptTask task)
        {
            LongTaskCompleted?.Invoke(task);
            AfterTaskCompleted(task);
        }

        private void OnBackgroundWorkerDisabled(Guid guid)
        {
            TaskHandlers.TryRemove(guid, out _);
        }

        #endregion

        public void AddJob(SharpEncryptTask sharpEncryptTask)
        {
            if (Cancelled)
                return;

            if (sharpEncryptTask == null)
                throw new ArgumentNullException(nameof(sharpEncryptTask));

            if (!sharpEncryptTask.IsLongRunning) {
                ShortTaskHandler.AddJob(sharpEncryptTask);
            }
            else
            {
                using (var newLongTaskHandler = new BackgroundTaskHandler())
                {
                    newLongTaskHandler.BackgroundWorkerDisabled += OnBackgroundWorkerDisabled;
                    newLongTaskHandler.TaskCompleted += LongTaskHandler_TaskCompletedEvent;
                    newLongTaskHandler.TaskDequeued += GenericTaskDequeued;

                    TaskHandlers.TryAdd(newLongTaskHandler.Identifier, newLongTaskHandler);

                    newLongTaskHandler.AddJob(sharpEncryptTask);
                }
            }
        }

        public void Dispose()
        {
            ShortTaskHandler.Dispose();
            foreach (var keyValuePair in TaskHandlers)
                keyValuePair.Value.Dispose();
        }

        private void AfterTaskCompleted(SharpEncryptTask task)
        {
            CompletedTasks.Add((task, DateTime.Now));
            if(Cancelled && ShortTaskHandler.TaskCount == 0 && TaskHandlers.IsEmpty)
            {
                TaskManagerCompleted?.Invoke();
            }
        }
    }
}