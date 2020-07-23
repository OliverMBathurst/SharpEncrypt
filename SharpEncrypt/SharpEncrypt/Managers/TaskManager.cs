using SharpEncrypt.AbstractClasses;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpEncrypt.Exceptions;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Managers
{
    public sealed class TaskManager : IDisposable
    {
        private readonly ConcurrentDictionary<Guid, BackgroundTaskManager> TaskHandlers = new ConcurrentDictionary<Guid, BackgroundTaskManager>();
        private readonly List<SharpEncryptTask> WaitingList = new List<SharpEncryptTask>();

        #region Delegates and events
        public delegate void TaskCompletedEventHandler(SharpEncryptTask task);
        public delegate void TaskDequeuedEventHandler(SharpEncryptTask task);
        public delegate void DuplicateExclusiveTaskEventHandler(SharpEncryptTask task);
        public delegate void ExceptionOccurredEventHandler(Exception exception);
        public delegate void TaskManagerCompletedEventHandler(bool hasRemainingTasks);

        public event TaskCompletedEventHandler TaskCompleted;
        public event TaskDequeuedEventHandler TaskDequeued;
        public event TaskManagerCompletedEventHandler TaskManagerCompleted;
        public event ExceptionOccurredEventHandler ExceptionOccurred;
        public event DuplicateExclusiveTaskEventHandler DuplicateExclusiveTask;
        #endregion

        #region Properties
        public bool Cancelled { get; private set; }

        public ConcurrentBag<(SharpEncryptTask Task, DateTime Time)> CompletedTasks { get; } = new ConcurrentBag<(SharpEncryptTask task, DateTime time)>();

        public bool HasCompletedBlockingTasks => TaskHandlers.All(x => x.Value.HasCompletedTasks) 
                                                 || TaskHandlers.All(x => x.Value.ActiveTasks.All(z => !z.ShouldBlockExit));

        public int TaskCount => TaskHandlers.Count;

        public int WaitingListTaskCount => WaitingList.Count;

        public IEnumerable<SharpEncryptTask> Tasks => TaskHandlers.SelectMany(x => x.Value.ActiveTasks);

        #endregion

        public TaskManager() => TaskCompleted += AfterTaskCompleted;

        #region Event methods

        private void OnTaskDequeued(SharpEncryptTask task) => TaskDequeued?.Invoke(task);

        private void OnTaskCompleted(SharpEncryptTask task) => TaskCompleted?.Invoke(task);

        private void OnBackgroundWorkerDisabled(Guid guid) => TaskHandlers.TryRemove(guid, out _);

        #endregion

        #region Methods

        public void AddTask(SharpEncryptTask sharpEncryptTask)
        {
            if (Cancelled)
            {
                ExceptionOccurred?.Invoke(new TaskManagerDisabledException());
                return;
            }

            if (sharpEncryptTask == null)
            {
                ExceptionOccurred?.Invoke(new ArgumentNullException(nameof(sharpEncryptTask)));
                return;
            }

            if (sharpEncryptTask.IsExclusive)
            {
                if (TaskHandlers.SelectMany(x => x.Value.ActiveTasks)
                    .Any(x => x.TaskType == sharpEncryptTask.TaskType))
                {
                    DuplicateExclusiveTask?.Invoke(sharpEncryptTask);
                }
            }

            if (BlockingTasks(sharpEncryptTask).Any())
            {
                WaitingList.Add(sharpEncryptTask);
            }
            else
            {
                AddBackgroundTaskManager(sharpEncryptTask);
            }
        }

        public void Dispose()
        {
            foreach (var keyValuePair in TaskHandlers)
                keyValuePair.Value.Dispose();
        }

        private void AfterTaskCompleted(SharpEncryptTask task)
        {
            CompletedTasks.Add((task, DateTime.Now));

            if (!Cancelled)
            {
                foreach (var waitingTask in WaitingList.Where(waitingTask => !BlockingTasks(waitingTask).Any()))
                {
                    WaitingList.Remove(waitingTask);
                    AddBackgroundTaskManager(waitingTask);
                }
            }
            else
            {
                TaskManagerCompleted?.Invoke(!TaskHandlers.IsEmpty);
            }
        }

        public void SetCancellationFlag() => Cancelled = true;

        public void CancelAllExisting(TaskType type)
        {
            foreach (var taskInstance in TaskHandlers.Select(x => x.Value.CurrentTaskInstance))
            {
                if (taskInstance.Task.TaskType == type)
                {
                    taskInstance.Token.Cancel();
                }
            }

            foreach (var matchingGuid in TaskHandlers.Select(x => x)
                .Where(x => x.Value.ActiveTasks.Any(z => z.TaskType == type))
                .Select(k => k.Key))
            {
                TaskHandlers.TryRemove(matchingGuid, out _);
            }
        }

        private void AddBackgroundTaskManager(SharpEncryptTask task)
        {
            using (var taskHandlerForTask = new BackgroundTaskManager())
            {
                taskHandlerForTask.BackgroundWorkerDisabled += OnBackgroundWorkerDisabled;
                taskHandlerForTask.TaskCompleted += OnTaskCompleted;
                taskHandlerForTask.TaskDequeued += OnTaskDequeued;
                taskHandlerForTask.Exception += exception => ExceptionOccurred?.Invoke(exception);

                TaskHandlers.TryAdd(taskHandlerForTask.Identifier, taskHandlerForTask);

                taskHandlerForTask.AddTask(task);
            }
        }

        private IEnumerable<SharpEncryptTask> BlockingTasks(ResourceBlocker blocker)
            => TaskHandlers
                .Select(x => x.Value)
                .SelectMany(z => z.ActiveTasks)
                .Where(k => k.ResourceType == blocker.ResourceType &&
                            k.BlockedResources.Any(blocker.BlockedResources.Contains));

        #endregion
    }
}