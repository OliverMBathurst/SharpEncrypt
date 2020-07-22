using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class ReadFolderExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadFolderExclusionListTask;

        public ReadFolderExclusionListTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(filePath)) return;
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FolderDataGridItemModel> models)
                    {
                        Result.Value = models;
                    }
                }
            });
        }
    }
}