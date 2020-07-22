using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class RenameFileNameTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.RenameFileTask;

        public RenameFileNameTask(string filePath, string hash, bool anonymise) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (anonymise)
                {
                    FileRenameHelper.AnonymizeFile(filePath, hash);
                }
                else
                {
                    FileRenameHelper.DeanonymizeFile(filePath, hash);
                }
            });
        }
    }
}