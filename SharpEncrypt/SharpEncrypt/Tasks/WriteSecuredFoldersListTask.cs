using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class WriteSecuredFoldersListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSecuredFoldersListTask;

        public WriteSecuredFoldersListTask(string filePath, bool add, params string[] directories)
            : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                var dirs = new List<string>();
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (new BinaryFormatter().Deserialize(fs) is List<string> folders)
                        {
                            dirs = folders;
                        }
                    }
                }

                foreach (var dir in directories)
                {
                    if (add)
                        dirs.Add(dir);
                    else
                        dirs.Remove(dir);
                }

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, dirs);
                }
            });
        }
    }
}