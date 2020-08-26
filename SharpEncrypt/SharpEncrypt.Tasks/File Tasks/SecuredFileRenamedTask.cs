using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    public sealed class SecuredFileRenamedTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.SecuredFileRenamedTask;

        public SecuredFileRenamedTask(string fileListFilePath, string newPath, string oldPath) : base(ResourceType.File, fileListFilePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(fileListFilePath))
                    return;

                var listOfModels = new List<FileModel>();                
                using (var fs = new FileStream(fileListFilePath, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileModel> list)
                    {
                        listOfModels = list;
                    }
                }

                if (!listOfModels.Any())
                    return;

                var oldModel = listOfModels.FirstOrDefault(x => x.Secured.Equals(oldPath, StringComparison.Ordinal));
                if (oldModel == null)
                    return;

                oldModel.Secured = newPath;

                using (var fs = new FileStream(fileListFilePath, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, listOfModels.Distinct().ToList());
                }

                Result.Value = new SecuredFileRenamedTaskResultModel { NewPath = newPath, OldPath = oldPath };
            });
        }
    }
}
