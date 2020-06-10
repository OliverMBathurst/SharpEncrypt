using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal sealed class SettingsWriter
    {
        public void Write(string filePath, SharpEncryptSettings sharpEncryptSettings) 
            => WriteSettingsFile(filePath, sharpEncryptSettings).Start();

        private Task WriteSettingsFile(string path, SharpEncryptSettings settings)
            => new Task(() => {
                if (File.Exists(path))
                    throw new IOException($"{path} already exists.");
                using (var fs = File.Create(path))
                    new BinaryFormatter().Serialize(fs, settings);
            });
    }
}
