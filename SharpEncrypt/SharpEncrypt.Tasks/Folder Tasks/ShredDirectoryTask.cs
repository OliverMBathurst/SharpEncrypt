using System.IO;
using System.Threading.Tasks;
using SecureEraseLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    public sealed class ShredDirectoryTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.ShredDirectoryTask;

        public ShredDirectoryTask(string directoryPath, bool includeSubfolders) : base(ResourceType.Folder, directoryPath)
        {
            InnerTask = new Task(() =>
            {
                if (Directory.Exists(directoryPath))
                    SecureEraseHelper.ShredDirectory(directoryPath, CipherType.Otp, includeSubfolders);
            });
        }
    }
}
