using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class WriteSettingsFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSettingsFileTask;

        public override SharpEncryptTaskResult Result { get; } = new SharpEncryptTaskResult { Type = typeof(bool) };

        public WriteSettingsFileTask(string path, SharpEncryptSettings settings)
        {
            InnerTask = new Task(() =>
            {
                var @return = true;
                try
                {
                    using (var fs = new FileStream(path, FileMode.Create))
                        new BinaryFormatter().Serialize(fs, settings);
                }
                catch (IOException)
                {
                    @return = false;
                }
                Result.Value = @return;
            });
        }
    }
}
