using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class InactivityTimeoutTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.InactivityTimeoutTask;

        public override bool ShouldBlockExit => false;

        public InactivityTimeoutTask(int msTimeout) : base(ResourceType.Undefined)
        {
            InnerTask = new Task(() =>
            {
                Thread.Sleep(msTimeout);
                Result.Value = msTimeout;
            });
        }
    }
}