using System;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Test_Tasks
{
    internal sealed class FailingTask : SharpEncryptTask
    {
        public FailingTask() : base(ResourceType.Undefined)
        {
            InnerTask = new Task(() => throw new ArgumentException("This is an exception produced by a test task."));
        }
    }
}
