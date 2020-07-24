using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class WriteFolderExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteFolderExclusionListTask;

        public WriteFolderExclusionListTask(string filePath, bool add, IEnumerable<FolderModel> models)
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

                var folders = new List<FolderModel>();
                if (!created)
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FolderModel> deserialized)
                        {
                            folders = deserialized;
                        }
                    }
                }

                if (add)
                {
                    folders.AddRange(models);
                }
                else
                {
                    if (folders.Any())
                    {
                        folders.RemoveAll(x => models.Any(z => z.Uri.Equals(x.Uri, StringComparison.Ordinal)));
                    }
                }

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, folders.Distinct().ToList());
                }
            });
        }
    }
}