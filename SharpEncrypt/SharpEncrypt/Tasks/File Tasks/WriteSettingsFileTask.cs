using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class WriteSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSettingsFileTask;

        public WriteSettingsFileTask(string path, SharpEncryptSettingsModel settings) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, settings);
                }
            });
        }
    }
}