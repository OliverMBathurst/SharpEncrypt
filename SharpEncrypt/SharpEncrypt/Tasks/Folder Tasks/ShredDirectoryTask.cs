using System.IO;
using System.Threading.Tasks;
using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    internal sealed class ShredDirectoryTask : SharpEncryptTask
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
