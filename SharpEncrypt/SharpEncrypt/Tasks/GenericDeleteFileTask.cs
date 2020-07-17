using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class GenericDeleteFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.GenericDeleteTask;

        public override bool IsSpecial => false;

        public GenericDeleteFileTask(string path) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() => { File.Delete(path); });
        }
    }
}
