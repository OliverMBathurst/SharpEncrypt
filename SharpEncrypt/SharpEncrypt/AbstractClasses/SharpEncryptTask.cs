using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTask : ResourceBlocker
    {
        public SharpEncryptTask(ResourceType resourceType, params string[] blockedResources) : base(resourceType, blockedResources) { }

        public virtual Guid Identifier { get; } = Guid.NewGuid();

        public virtual bool IsLongRunning { get; set; } = false;

        public virtual TaskType TaskType { get; }

        public virtual Task InnerTask { get; set; }

        public virtual void Start() => InnerTask.Start();

        public virtual void Wait(CancellationToken token) => InnerTask.Wait(token);

        public virtual SharpEncryptTaskResult Result { get; set; } = new SharpEncryptTaskResult();
    }
}