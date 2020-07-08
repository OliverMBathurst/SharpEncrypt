using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var created = false;
                if (!File.Exists(filePath))
                {
                    using (var _ = File.Create(filePath)) { }
                    created = true;
                    if (!add)
                        return;
                }

                var dirs = new List<string>();
                if (!created)
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<string> folders)
                        {
                            dirs = folders;
                        }
                    }
                }

                if (add)
                {
                    dirs.AddRange(directories);
                }
                else
                {
                    if (dirs.Any())
                    {
                        dirs.RemoveAll(x => directories.Any(z => z.Equals(x, StringComparison.Ordinal)));
                    }
                }
                

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, dirs.Distinct().ToList());
                }

                if (add)
                    Result.Value = dirs;
            });
        }
    }
}