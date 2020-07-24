using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class WriteFileExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteFileExclusionListTask;

        public WriteFileExclusionListTask(string filePath, bool add, IEnumerable<FileModel> excludedFiles) : base(ResourceType.File, filePath)
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

                var files = new List<FileModel>();
                if (!created)
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileModel> deserialized)
                        {
                            files = deserialized;
                        }
                    }
                }

                if (add)
                {
                    files.AddRange(excludedFiles);
                }
                else
                {
                    if (files.Any())
                    {
                        files.RemoveAll(x => excludedFiles.Any(z => z.Equals(x)));
                    }
                }

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, files.Distinct().ToList());
                }
            });
        }
    }
}