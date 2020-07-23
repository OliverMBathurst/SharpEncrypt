using System.Threading;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Test_Tasks
{
    internal sealed class LongRunningTask : SharpEncryptTask
    {
        public LongRunningTask() : base(ResourceType.Undefined)
        {
            InnerTask = new Task(() =>
            {
                Thread.Sleep(20000);
            });
        }
    }
}
