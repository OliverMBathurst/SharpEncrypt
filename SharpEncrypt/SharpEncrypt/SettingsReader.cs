using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpEncrypt
{
    internal sealed class SettingsReader
    {
        public SharpEncryptSettings ReadSettingsFile(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"{path} does not exist.");

            using (var fs = File.Open(path, FileMode.Open))
                if (new BinaryFormatter().Deserialize(fs) is SharpEncryptSettings settings)
                    return settings;

            return null;
        }
    }
}