using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class WriteSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSettingsFileTask;

        public WriteSettingsFileTask(string path, SharpEncryptSettings settings)
            : base(ResourceType.File, path)
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
