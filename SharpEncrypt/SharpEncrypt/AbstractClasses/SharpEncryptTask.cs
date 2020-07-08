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

        public virtual bool IsSpecial { get; set; } = true;

        //whether only one task of this type should be running at any one point or not
        public virtual bool IsExclusive { get; set; } = false;

        public virtual TaskType TaskType { get; } = TaskType.Undefined;

        public virtual Task InnerTask { get; set; }

        public virtual void Start() => InnerTask.Start();

        public virtual void Wait(CancellationToken token) => InnerTask.Wait(token);

        public virtual SharpEncryptTaskResult Result { get; set; } = new SharpEncryptTaskResult();
    }
}