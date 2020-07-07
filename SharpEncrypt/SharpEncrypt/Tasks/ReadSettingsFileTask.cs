using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReadSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSettingsFileTask;

        public ReadSettingsFileTask(string path) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is SharpEncryptSettings settings)
                        {
                            Result.Value = settings;
                        }
                    }
                }
            });
        }
    }
}