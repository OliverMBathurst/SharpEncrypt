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

        public ReadSettingsFileTask(string path)
        {
            InnerTask = new Task(() =>
            {                                
                using (var fs = File.Open(path, FileMode.Open))
                {
                    Value = new BinaryFormatter().Deserialize(fs);
                }
            });
        }
    }
}