using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ReadUncontainerizedFilesListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadUncontainerizedFilesListTask;

        public override bool IsExclusive => true;

        public ReadUncontainerizedFilesListTask(string path) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(path)) return;

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    if (new BinaryFormatter().Deserialize(fs) is List<FolderModel> paths)
                    {
                        Result.Value = paths;
                    }
                }
            });
        }
    }
}
