using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class ReadSecuredFoldersListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSecuredFoldersListTask;

        public ReadSecuredFoldersListTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() => 
            {
                var foldersList = new List<FolderModel>();
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FolderModel> paths)
                        {
                            foldersList = paths;
                        }
                    }
                }

                Result.Value = foldersList;
            });
        }
    }
}