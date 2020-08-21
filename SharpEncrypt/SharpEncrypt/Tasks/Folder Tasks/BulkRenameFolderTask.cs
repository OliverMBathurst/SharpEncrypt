using System;
using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class BulkRenameFolderTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.BulkRenameFolderTask;

        public BulkRenameFolderTask(string folderUri, string sessionPasswordHash, bool includeSubfolders, bool anonymize) : base(ResourceType.Folder, folderUri)
        {
            InnerTask = new Task(() =>
            {
                if (folderUri == null)
                    throw new ArgumentNullException(nameof(folderUri));
                if (sessionPasswordHash == null)
                    throw new ArgumentNullException(nameof(sessionPasswordHash));
                if (!Directory.Exists(folderUri))
                    throw new DirectoryNotFoundException(nameof(folderUri));

                RenameFilesInFolder(folderUri);

                void RenameFilesInFolder(string uri)
                {
                    foreach (var file in Directory.GetFiles(uri))
                    {
                        if (anonymize)
                        {
                            FileRenameHelper.AnonymizeFile(file, sessionPasswordHash);
                        }
                        else
                        {
                            FileRenameHelper.DeanonymizeFile(file, sessionPasswordHash);
                        }
                    }

                    if (!includeSubfolders) return;

                    foreach (var subFolder in Directory.GetDirectories(uri))
                    {
                        RenameFilesInFolder(subFolder);
                    }
                }
            });
        }
    }
}