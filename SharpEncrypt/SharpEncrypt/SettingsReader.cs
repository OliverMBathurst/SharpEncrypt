using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal static class SettingsReader
    {
        public static Task<SharpEncryptSettings> ReadSettingsFile(string path, bool synchronous)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

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