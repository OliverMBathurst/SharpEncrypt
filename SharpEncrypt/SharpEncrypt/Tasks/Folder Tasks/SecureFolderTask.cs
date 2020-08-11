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

        public SecureFolderTask(string folderPath, ContainerizationSettings settings, bool includeSubFolders) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() =>
            {
                var model = new FolderModel
                {
                    Uri = folderPath,
                    FileModels = DirectoryHelper.EnumerateAndSecureFiles(folderPath, settings).ToList()
                };

                if (includeSubFolders)
                    model.SubFolders = DirectoryHelper.EnumerateAndSecureSubFolders(folderPath, settings).ToList();

                Result.Value = model;
            });
        }
    }
}
