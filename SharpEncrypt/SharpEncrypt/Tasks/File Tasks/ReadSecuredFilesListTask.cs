using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ReadSecuredFilesListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSecuredFilesListTask;
    
        public ReadSecuredFilesListTask(string path) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(path)) return;
                using (var fs = new FileStream(path, FileMode.Open))
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
