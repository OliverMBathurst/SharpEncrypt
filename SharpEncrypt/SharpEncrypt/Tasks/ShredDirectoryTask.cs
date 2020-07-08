using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ShredDirectoryTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ShredDirectoryTask;

        public ShredDirectoryTask(string directoryPath, bool includeSubfolders) : base(ResourceType.Folder, directoryPath)
        {
            InnerTask = new Task(() =>
            {
                if (Directory.Exists(directoryPath))
                {
                    SecureEraseHelper.ShredDirectory(directoryPath, CipherType.OTP, includeSubfolders);
                }
            });
        }
    }
}
