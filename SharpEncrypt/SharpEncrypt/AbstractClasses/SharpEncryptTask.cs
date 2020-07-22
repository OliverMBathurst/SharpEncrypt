using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTask : ResourceBlocker
    {
        protected SharpEncryptTask(ResourceType resourceType, params string[] blockedResources) : base(resourceType, blockedResources) { }

        protected SharpEncryptTask(ResourceType resourceType, IEnumerable<string> blockedResources) : base(resourceType, blockedResources) { }

        public bool Disabled { get; set; } = false;

        public virtual bool ShouldBlockExit { get; set; } = true;

        public virtual bool IsExclusive { get; set; } = false;

        public virtual TaskType TaskType { get; } = TaskType.Undefined;

        public virtual Task InnerTask { get; set; }

        public virtual void Start() => InnerTask.Start();

        public virtual void Wait(CancellationToken token) => InnerTask.Wait(token);

        public virtual SharpEncryptTaskResult Result { get; set; } = new SharpEncryptTaskResult();
    }
}