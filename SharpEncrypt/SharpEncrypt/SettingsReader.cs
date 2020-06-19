using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal static class SettingsReader
    {
        public static Task<SharpEncryptSettings> ReadSettingsFileTask(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return new Task<SharpEncryptSettings>(() =>
            {
                using (var fs = File.Open(path, FileMode.Open))
                {
                    if (new BinaryFormatter().Deserialize(fs) is SharpEncryptSettings settings)
                        return settings;
                    else
                        return null;
                }
            });
        }
    }
}