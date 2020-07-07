using SharpEncrypt.AbstractClasses;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpEncrypt.Exceptions;

namespace SharpEncrypt
{
    public sealed class TaskManager : IDisposable
    {
        private readonly BackgroundTaskHandler SpecialTaskHandler = new BackgroundTaskHandler(false);
        private readonly ConcurrentDictionary<Guid, BackgroundTaskHandler> GenericTaskHandlers = new ConcurrentDictionary<Guid, BackgroundTaskHandler>();

        #region Delegates and events
        public delegate void TaskCompletedEventHandler(SharpEncryptTask task);
        public delegate void TaskDequeuedEventHandler(SharpEncryptTask task);
        public delegate void ExceptionOccurredEventHandler(Exception exception);
        public delegate void TaskManagerCompletedEventHandler();

        public event TaskCompletedEventHandler TaskCompleted;
        public event TaskDequeuedEventHandler TaskDequeued;
        public event TaskManagerCompletedEventHandler TaskManagerCompleted;
        public event ExceptionOccurredEventHandler ExceptionOccurred;
        #endregion

        public TaskManager()
        {
            SpecialTaskHandler.TaskCompleted += OnTaskCompleted;
            SpecialTaskHandler.TaskDequeued += OnTaskDequeued;
            SpecialTaskHandler.ExceptionOccurred += OnException;
        }

        #region Properties
        public bool Cancelled { get; private set; } = false;

        public ConcurrentBag<(SharpEncryptTask Task, DateTime Time)> CompletedTasks { get; } = new ConcurrentBag<(SharpEncryptTask task, DateTime time)>();

        public bool HasCompletedJobs => SpecialTaskHandler.HasCompletedJobs && GenericTaskHandlers.All(x => x.Value.HasCompletedJobs);

        public int TaskCount => SpecialTaskHandler.TaskCount + GenericTaskHandlers.Count;

        public IEnumerable<SharpEncryptTask> Tasks => SpecialTaskHandler.Tasks.Concat(GenericTaskHandlers.SelectMany(x => x.Value.Tasks));

        #endregion

        #region Event methods

        private void OnException(Exception exception)
        {
            ExceptionOccurred?.Invoke(exception);
        }

        private void OnTaskDequeued(SharpEncryptTask task)
        {
            TaskDequeued?.Invoke(task);
        }

        private void OnTaskCompleted(SharpEncryptTask task)
        {
            TaskCompleted?.Invoke(task);
            AfterTaskCompleted(task);
        }

        private void OnBackgroundWorkerDisabled(Guid guid)
        {
            GenericTaskHandlers.TryRemove(guid, out _);
        }

        #endregion

        #region Methods

        public void AddTask(SharpEncryptTask sharpEncryptTask)
        {
            if (Cancelled)
            {
                OnException(new TaskManagerDisabledException());
                return;
            }

            if (sharpEncryptTask == null)
            {
                OnException(new ArgumentNullException(nameof(sharpEncryptTask)));
                return;
            }

            if (sharpEncryptTask.IsSpecial)
            {
                SpecialTaskHandler.AddTask(sharpEncryptTask);
            }
            else
            {
                using (var taskHandlerForTask = new BackgroundTaskHandler())
                {
                    taskHandlerForTask.BackgroundWorkerDisabled += OnBackgroundWorkerDisabled;
                    taskHandlerForTask.TaskCompleted += OnTaskCompleted;
                    taskHandlerForTask.TaskDequeued += OnTaskDequeued;
                    taskHandlerForTask.ExceptionOccurred += OnException;

                    GenericTaskHandlers.TryAdd(taskHandlerForTask.Identifier, taskHandlerForTask);
                    
                    taskHandlerForTask.AddTask(sharpEncryptTask);
                }
            }
        }

        public void Dispose()
        {
            SpecialTaskHandler.Dispose();
            foreach (var keyValuePair in GenericTaskHandlers)
                keyValuePair.Value.Dispose();
        }

        private void AfterTaskCompleted(SharpEncryptTask task)
        {
            CompletedTasks.Add((task, DateTime.Now));
            if(Cancelled && SpecialTaskHandler.TaskCount == 0 && GenericTaskHandlers.IsEmpty)
            {
                TaskManagerCompleted?.Invoke();
            }
        }

        public void SetCancellationFlag() => Cancelled = true;

        #endregion
    }
}