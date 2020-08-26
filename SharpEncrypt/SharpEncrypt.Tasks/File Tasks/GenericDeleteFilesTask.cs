using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    public sealed class GenericDeleteFilesTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.GenericDeleteFilesTask;

        public GenericDeleteFilesTask(params string[] paths) : base(ResourceType.File, paths)
        {
            InnerTask = new Task(() =>
            {
                foreach (var filePath in paths)
                    File.Delete(filePath);
            });
        }
    }
}
