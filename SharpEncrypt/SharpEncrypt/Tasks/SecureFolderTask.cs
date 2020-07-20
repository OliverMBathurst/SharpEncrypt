using AESLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTask
    {
        public override bool IsSpecial => false;

        public override TaskType TaskType => TaskType.SecureFolderTask;

        public SecureFolderTask(string folderPath, string password, bool includeSubFolders) : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() => {

                SecureDirectory(folderPath);

                void SecureDirectory(string dir)
                {
                    foreach (var filePath in Directory.GetFiles(dir))
                    {
                        ContainerHelper.ContainerizeFile(filePath, AESHelper.GetNewAESKey(), password);
                    }

                    if (includeSubFolders)
                    {
                        foreach (var subFolder in Directory.GetDirectories(dir))
                        {
                            SecureDirectory(subFolder);
                        }
                    }
                }

                Result.Value = new FolderDataGridItemModel { URI = folderPath, Time = DateTime.Now };
            });
        }
    }
}
