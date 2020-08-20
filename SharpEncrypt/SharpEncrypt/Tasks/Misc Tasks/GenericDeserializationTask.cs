using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.AbstractClasses;

namespace SharpEncrypt.Tasks.Misc_Tasks
{
    internal sealed class GenericDeserializationTask<T> : SharpEncryptTask
    {
        public GenericDeserializationTask(string path, TaskType taskType)
            : base(ResourceType.File, taskType, path)
            => InnerTask = GetDeserializationTask(path, default);

        public GenericDeserializationTask(string path, TaskType taskType, T defaultValue)
            : base(ResourceType.File, taskType, path)
            => InnerTask = GetDeserializationTask(path, defaultValue);

        public GenericDeserializationTask(string path, TaskType taskType, params TaskType[] blockingTaskTypes)
            : base(ResourceType.File, path, taskType, blockingTaskTypes)
            => InnerTask = GetDeserializationTask(path, default);

        private Task GetDeserializationTask(string path, T defaultValue) 
            => new Task(() =>
            {
                if (!File.Exists(path)) return;

                Result.Value = defaultValue;
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is T settings)
                    {
                        Result.Value = settings;
                    }
                }
            });
    }
}