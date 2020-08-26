using System;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Test_Tasks
{
    public sealed class FailingTask : SharpEncryptTaskModel
    {
        public FailingTask() : base(ResourceType.Undefined)
        {
            InnerTask = new Task(() => throw new ArgumentException("This is an exception produced by a test task."));
        }
    }
}
