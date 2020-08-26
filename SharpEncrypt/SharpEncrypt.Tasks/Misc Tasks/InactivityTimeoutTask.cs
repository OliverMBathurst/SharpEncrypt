using System.Threading;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Misc_Tasks
{
    public sealed class InactivityTimeoutTask : SharpEncryptTaskModel
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