using System;
using System.IO;
using System.Threading.Tasks;
using AesLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.SecureFolderTask;

        public SecureFolderTask(string folderPath, string password, bool includeSubFolders) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() => {

                SecureDirectory(folderPath);

                void SecureDirectory(string dir)
                {
                    foreach (var filePath in Directory.GetFiles(dir))
                    {
                        ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
                    }

                    if (!includeSubFolders) return;
                    foreach (var subFolder in Directory.GetDirectories(dir))
                    {
                        SecureDirectory(subFolder);
                    }
                }

                Result.Value = new FolderDataGridItemModel { Uri = folderPath, Time = DateTime.Now };
            });
        }
    }
}
