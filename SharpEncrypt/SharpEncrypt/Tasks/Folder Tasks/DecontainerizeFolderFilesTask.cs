using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class DecontainerizeFolderFilesTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.DecontainerizeFolderFilesTask;

        public DecontainerizeFolderFilesTask(FolderModel model, ContainerizationSettingsModel settingsModel, bool includeSubFolders, bool removeAfter, bool temporary) : base(ResourceType.Folder, model.Uri)
        {
            InnerTask = new Task(() =>
            {
                DirectoryHelper.DecontainerizeDirectoryFiles(model, model.Uri, settingsModel, includeSubFolders);

                Result.Value = new DecontainerizeFolderFilesTaskResultModel
                {
                    Model = model, 
                    RemoveAfter = removeAfter,
                    Temporary = temporary
                };
            });
        }
    }
}
