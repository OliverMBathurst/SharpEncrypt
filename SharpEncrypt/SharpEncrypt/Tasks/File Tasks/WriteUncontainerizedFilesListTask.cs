using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class WriteUncontainerizedFilesListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteUncontainerizedFilesListTask;

        public override bool IsExclusive => true;

        public WriteUncontainerizedFilesListTask(string path, IReadOnlyCollection<FolderModel> filePaths, bool exitAfter, bool silent) 
            : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, filePaths);
                }

                Result.Value = new FinalizableTaskResultModel
                {
                    ExitAfter = exitAfter,
                    Silent = silent
                };
            });
        }
    }
}
