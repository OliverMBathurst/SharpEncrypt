using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal static class SettingsWriter
    {
        public static Task WriteSettingsFileTask(string path, SharpEncryptSettings settings)
            => new Task(() => {
                using (var fs = new FileStream(path, FileMode.Create))
                    new BinaryFormatter().Serialize(fs, settings);
            });
    }
}
