using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt
{
    internal static class SettingsWriter
    {
        public static Task WriteSettingsFile(string path, SharpEncryptSettings settings, bool synchronous)
        {
            var task = new Task(() => {
                using (var fs = new FileStream(path, FileMode.Create))
                    new BinaryFormatter().Serialize(fs, settings);
            });

            if (synchronous)
                task.RunSynchronously();
            else
                task.Start();
            return task;
        }
    }
}
