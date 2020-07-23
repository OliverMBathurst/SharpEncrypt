using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class GenericDeleteFilesTask : SharpEncryptTask
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
