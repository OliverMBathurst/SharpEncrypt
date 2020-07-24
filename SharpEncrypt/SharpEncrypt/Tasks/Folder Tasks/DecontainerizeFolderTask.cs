using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileGeneratorLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class DecontainerizeFolderTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeFolderTask;

        public DecontainerizeFolderTask(FolderModel model, string password, bool includeSubFolders, bool removeAfter, bool temporary) : base(ResourceType.Folder, model.Uri)
        {
            InnerTask = new Task(() =>
            {
                DecontainerizeDirectory(model.Uri);

                void DecontainerizeDirectory(string dir)
                {
                    foreach (var filePath in Directory.GetFiles(dir))
                    {
                        ContainerHelper.DecontainerizeFile(filePath, password);

                        var fileModel = model.FileModels.FirstOrDefault(x => x.Secured.Equals(filePath));

                        var ext = string.Empty;
                        if (fileModel != null)
                            ext = Path.GetExtension(fileModel.File);

                        var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                            Path.GetDirectoryName(filePath),
                            Path.GetFileNameWithoutExtension(filePath),
                            ext);

                        File.Move(filePath, newPath);
                    }

                    if (!includeSubFolders) return;

                    foreach (var subFolder in Directory.GetDirectories(dir))
                    {
                        DecontainerizeDirectory(subFolder);
                    }
                }

                Result.Value = new DecontainerizeFolderTaskResult
                {
                    Model = model, 
                    RemoveAfter = removeAfter,
                    Temporary = temporary
                };
            });
        }
    }
}
