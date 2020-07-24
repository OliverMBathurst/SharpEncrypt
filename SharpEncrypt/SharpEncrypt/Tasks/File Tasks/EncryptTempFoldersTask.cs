using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AesLibrary;
using FileGeneratorLibrary;
using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class EncryptTempFoldersTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.EncryptTempFoldersTask;

        public override bool IsExclusive => true;

        public EncryptTempFoldersTask(IReadOnlyCollection<FolderModel> models, string password, string ext, bool includeSubFolders, bool exitAfter, bool silent) : base(ResourceType.Folder, models.Select(x => x.Uri))
        {
            InnerTask = new Task(() =>
            {
                var uncontainerized = new List<FolderModel>();
                var containerized = new List<FolderModel>();

                foreach (var model in models)
                    ContainerizeFolder(model.Uri);

                Result.Value = new EncryptTempFoldersTaskResultModel
                {
                    ContainerizedFolders = containerized,
                    UncontainerizedFolders = uncontainerized,
                    ExitAfter = exitAfter,
                    Silent = silent
                };

                void ContainerizeFolder(string folderPath)
                {
                    var folder = new FolderModel { Uri = folderPath };
                    try
                    {
                        foreach (var filePath in Directory.GetFiles(folderPath))
                        {
                            ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
                            var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                                Path.GetDirectoryName(filePath),
                                Path.GetFileNameWithoutExtension(filePath),
                                ext);

                            File.Move(filePath, newPath);

                            folder.FileModels.Add(new FileModel
                            {
                                File = Path.GetFileName(filePath),
                                Time = DateTime.Now,
                                Secured = newPath,
                                Algorithm = CipherType.Aes
                            });
                        }

                        if (includeSubFolders)
                        {
                            foreach (var subFolder in Directory.GetDirectories(folderPath))
                            {
                                ContainerizeFolder(subFolder);
                            }
                        }

                        containerized.Add(folder);
                    }
                    catch (Exception)
                    {
                        uncontainerized.Add(folder);
                    }
                }
            });
        }
    }
}
