using System.IO;
using System.Threading.Tasks;
using GutmannLibrary;
using SecureEraseLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    public sealed class SecureDeleteFileTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.SecureDeleteFileTask;

        public SecureDeleteFileTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                var newFilePath = SecureEraseHelper.ObfuscateFileName(filePath);
                SecureEraseHelper.ObfuscateFileProperties(newFilePath);
                GutmannHelper.WipeFile(newFilePath);
                File.Delete(newFilePath);
            });
        }
    }
}
