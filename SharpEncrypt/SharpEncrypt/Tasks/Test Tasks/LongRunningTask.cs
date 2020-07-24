﻿using System.Threading;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Test_Tasks
{
    internal sealed class LongRunningTask : SharpEncryptTask
    {
        public override bool ShouldBlockExit { get; set; }

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
