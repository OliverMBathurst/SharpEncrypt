using System;
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
    internal sealed class OnSecuredFileRenamedTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OnSecuredFileRenamedTask;

        public OnSecuredFileRenamedTask(string fileListFilePath, string newPath, string oldPath) : base(ResourceType.File, fileListFilePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(fileListFilePath))
                    return;

                var listOfModels = new List<FileDataGridItemModel>();                
                using (var fs = new FileStream(fileListFilePath, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileDataGridItemModel> list)
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

                Result.Value = new OnSecuredFileRenamedTaskResult { NewPath = newPath, OldPath = oldPath };
            });
        }
    }
}
