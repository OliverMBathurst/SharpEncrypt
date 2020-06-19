using SharpEncrypt.Enums;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal sealed class TaskHandler
    {
        private readonly ConcurrentDictionary<TaskType, ConcurrentQueue<Task>> Tasks 
            = new ConcurrentDictionary<TaskType, ConcurrentQueue<Task>>();

        public bool HasCompletedJobs => Tasks.IsEmpty;

        public void AddJob(TaskType type, Task task)
        {
            if (!Tasks.ContainsKey(type))
                Tasks.TryAdd(type, new ConcurrentQueue<Task>());
            Tasks[type].Enqueue(task);
        }

        public void StartBackgroundTask() => BackgroundTask.Start();

        private Task BackgroundTask
            => new Task(() =>
            {
                var tasks = new List<Task>();
                while (true)
                {
                    foreach(var kvp in Tasks)
                        if (kvp.Value.Any())
                            if(kvp.Value.TryDequeue(out var result))
                                tasks.Add(result);

                    foreach (var task in tasks)
                        task.Start();
                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }
            });
    }
}
