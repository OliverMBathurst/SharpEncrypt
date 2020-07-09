using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTask
    {
        public override bool IsSpecial => false;

        public override TaskType TaskType => TaskType.SecureFolderTask;

        public SecureFolderTask(string folderPath) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() => {
                //logic here
                Result.Value = new FolderDataGridItemModel { URI = folderPath, Time = DateTime.Now };
            });
        }
    }
}
