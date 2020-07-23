using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ReadSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSettingsFileTask;

        public ReadSettingsFileTask(string path, params TaskType[] blockingTaskTypes) : base(ResourceType.File, new []{ path }, blockingTaskTypes)
        {
            InnerTask = new Task(() =>
            {
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is SharpEncryptSettingsModel settings)
                        {
                            Result.Value = settings;
                        }
                    }
                }

                if (Result.Value == null)
                    Result.Value = new SharpEncryptSettingsModel();
            });
        }
    }
}