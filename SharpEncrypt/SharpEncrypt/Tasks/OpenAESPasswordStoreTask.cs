using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OpenAESPasswordStoreTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OpenAESPasswordStoreTask;

        public override bool IsSpecial => false;

        public OpenAESPasswordStoreTask(string filePath, string password) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(filePath))
                {
                    using (_ = File.Create(filePath)) { }
                }

                //todo
            });
        }
    }
}
