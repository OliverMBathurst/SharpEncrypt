using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal sealed class SettingsReader
    {
        public Task<SharpEncryptSettings> ReadSettingsFile(string path, bool synchronous)
        {
            if (!File.Exists(path))
                throw new IOException($"{path} does not exist.");

            var task = new Task<SharpEncryptSettings>(() =>
            {
                using (var fs = File.Open(path, FileMode.Open))
                {
                    if (new BinaryFormatter().Deserialize(fs) is SharpEncryptSettings settings)
                        return settings;
                    else
                        return null;
                }
            });

            if (synchronous)
                task.RunSynchronously();
            else
                task.Start();

            return task;
        }
    }
}