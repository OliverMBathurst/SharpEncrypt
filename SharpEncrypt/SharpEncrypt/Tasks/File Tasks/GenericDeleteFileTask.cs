using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class GenericDeleteFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.GenericDeleteTask;

        public GenericDeleteFileTask(string path) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() => { File.Delete(path); });
        }
    }
}
