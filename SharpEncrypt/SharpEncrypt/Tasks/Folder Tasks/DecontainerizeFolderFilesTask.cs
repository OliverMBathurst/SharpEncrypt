using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class DecontainerizeFolderFilesTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeFolderFilesTask;

        public DecontainerizeFolderFilesTask(FolderModel model, string password, string encryptedFileExtension, bool includeSubFolders, bool removeAfter, bool temporary) : base(ResourceType.Folder, model.Uri)
        {
            InnerTask = new Task(() =>
            {
                DirectoryHelper.DecontainerizeDirectoryFiles(model, model.Uri, password, encryptedFileExtension, includeSubFolders);

                Result.Value = new DecontainerizeFolderFilesTaskResult
                {
                    Model = model, 
                    RemoveAfter = removeAfter,
                    Temporary = temporary
                };
            });
        }
    }
}
