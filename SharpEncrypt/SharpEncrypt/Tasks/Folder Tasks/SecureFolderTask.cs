using System;
using System.IO;
using System.Threading.Tasks;
using AesLibrary;
using FileGeneratorLibrary;
using SecureEraseLibrary;
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
                var model = new FolderModel { Uri = folderPath };

                SecureDirectory(folderPath);

                void SecureDirectory(string dir)
                {
                    foreach (var filePath in Directory.GetFiles(dir))
                    {
                        ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
                        var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                            Path.GetDirectoryName(filePath),
                            Path.GetFileNameWithoutExtension(filePath), 
                            ext);

                        File.Move(filePath, newPath);

                        model.FileModels.Add(new FileModel
                        {
                            File = Path.GetFileName(filePath),
                            Time = DateTime.Now,
                            Secured = newPath,
                            Algorithm = CipherType.Aes,
                            InSubfolder = dir != folderPath
                        });
                    }

                    if (!includeSubFolders) return;
                    foreach (var subFolder in Directory.GetDirectories(dir))
                    {
                        SecureDirectory(subFolder);
                    }
                }

                Result.Value = model;
            });
        }
    }
}
