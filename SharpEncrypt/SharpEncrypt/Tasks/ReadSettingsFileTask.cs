using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReadSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSettingsFileTask;

        public override SharpEncryptTaskResult Result { get; } = new SharpEncryptTaskResult { Type = typeof(SharpEncryptSettings) };

        public ReadSettingsFileTask(string path)
        {
            InnerTask = new Task(() =>
            {                                
                using (var fs = File.Open(path, FileMode.Open))
                {
                    Result.Value = new BinaryFormatter().Deserialize(fs);
                }
            });
        }
    }
}