using System.Threading;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Test_Tasks
{
    internal sealed class LongRunningTask : SharpEncryptTaskModel
    {
        public LongRunningTask(bool blocking) : base(ResourceType.Undefined)
        {
            ShouldBlockExit = blocking;
            InnerTask = new Task(() =>
            {
                Thread.Sleep(20000);
            });
        }
    }
}
