using System.Linq;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.SecureFolderTask;

        public SecureFolderTask(string folderPath, ContainerizationSettingsModel settingsModel, bool includeSubFolders) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() =>
            {
                var model = new FolderModel
                {
                    Uri = folderPath,
                    FileModels = DirectoryHelper.EnumerateAndSecureFiles(folderPath, settingsModel).ToList()
                };

                if (includeSubFolders)
                    model.SubFolders = DirectoryHelper.EnumerateAndSecureSubFolders(folderPath, settingsModel).ToList();

                Result.Value = model;
            });
        }
    }
}
