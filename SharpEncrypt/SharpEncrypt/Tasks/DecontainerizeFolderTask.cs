using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class DecontainerizeFolderTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeFolderTask;

        public override bool IsSpecial => false;

        public DecontainerizeFolderTask(FolderDataGridItemModel model, string password, bool includeSubFolders, bool removeAfter = false) : base(ResourceType.Folder, model.URI)
        {
            InnerTask = new Task(() =>
            {
                DecontainerizeDirectory(model.URI);

                void DecontainerizeDirectory(string dir)
                {
                    foreach (var filePath in Directory.GetFiles(dir))
                    {
                        ContainerHelper.DecontainerizeFile(filePath, password);
                    }

                    if (includeSubFolders)
                    {
                        foreach (var subFolder in Directory.GetDirectories(dir))
                        {
                            DecontainerizeDirectory(subFolder);
                        }
                    }
                }

                Result.Value = new DecontainerizeFolderTaskResult { Model = model, RemoveAfter = removeAfter };
            });
        }
    }
}
