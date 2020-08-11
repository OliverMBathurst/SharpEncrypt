using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public EncryptTempFoldersTask(IReadOnlyCollection<FolderModel> models, ContainerizationSettings settings, bool includeSubFolders, bool exitAfter, bool silent) : base(ResourceType.Folder, models.Select(x => x.Uri))
        {
            InnerTask = new Task(() =>
            {
                var uncontainerized = new List<FolderModel>();
                var containerized = new List<FolderModel>();

                foreach (var model in models)
                    SecureFolder(model.Uri);

                Result.Value = new EncryptTempFoldersTaskResultModel
                {
                    ContainerizedFolders = containerized,
                    UncontainerizedFolders = uncontainerized,
                    ExitAfter = exitAfter,
                    Silent = silent
                };

                void SecureFolder(string folderUri)
                {
                    var folderModel = new FolderModel
                    {
                        Uri = folderUri
                    };

                    try
                    {
                        folderModel.FileModels = DirectoryHelper.EnumerateAndSecureFiles(folderUri, settings).ToList();

                        if (includeSubFolders)
                            folderModel.SubFolders = DirectoryHelper.EnumerateAndSecureSubFolders(folderUri, settings)
                                .ToList();

                        containerized.Add(folderModel);
                    }
                    catch (Exception)
                    {
                        uncontainerized.Add(folderModel);
                    }
                }
            });
        }
    }
}
