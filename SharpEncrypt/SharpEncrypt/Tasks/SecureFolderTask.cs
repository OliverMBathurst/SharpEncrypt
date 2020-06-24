using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class SecureFolderTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.SecureFolderTask;

        public override SharpEncryptTaskResult Result { get; } = new SharpEncryptTaskResult { Type = typeof(string) };

        public SecureFolderTask(string folderPath)
        {
            InnerTask = new Task(() => {
                //logic here
                Result.Value = folderPath;
            });
        }
    }
}
