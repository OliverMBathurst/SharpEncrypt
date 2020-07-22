using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ReadFileExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadFileExclusionListTask;

        public ReadFileExclusionListTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(filePath)) return;
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileDataGridItemModel> models)
                    {
                        Result.Value = models;
                    }
                }
            });
        }
    }
}