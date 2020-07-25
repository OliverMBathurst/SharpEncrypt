using System.Linq;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.SecureFolderTask;

        public SecureFolderTask(string folderPath, string password, bool includeSubFolders, string ext) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() =>
            {
                var model = new FolderModel
                {
                    Uri = folderPath,
                    FileModels = DirectoryHelper.EnumerateAndSecureFiles(folderPath, password, ext).ToList()
                };

                if (includeSubFolders)
                    model.SubFolders = DirectoryHelper.EnumerateAndSecureSubFolders(folderPath, password, ext).ToList();

                Result.Value = model;
            });
        }
    }
}
