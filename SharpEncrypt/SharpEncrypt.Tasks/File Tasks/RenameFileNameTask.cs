using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    public sealed class RenameFileNameTask : SharpEncryptTaskModel
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