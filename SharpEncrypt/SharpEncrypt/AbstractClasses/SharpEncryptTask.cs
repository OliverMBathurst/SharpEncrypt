using System;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTask : ResourceBlocker
    {
        protected SharpEncryptTask(ResourceType resourceType, TaskType taskType, params string[] blockedResources) :
            base(resourceType, blockedResources)
            => TaskType = taskType;

        protected SharpEncryptTask(ResourceType resourceType, params string[] blockedResources) : base(resourceType, blockedResources) { }

        protected SharpEncryptTask(ResourceType resourceType, IEnumerable<string> blockedResources) : base(resourceType, blockedResources) { }

        protected SharpEncryptTask(ResourceType resourceType, IEnumerable<string> blockedResources,
            params TaskType[] blockingTaskTypes) : base(resourceType, blockedResources, blockingTaskTypes) { }

        protected SharpEncryptTask(ResourceType resourceType, string blockedResource, TaskType taskType,
            params TaskType[] blockingTaskTypes) : base(resourceType, blockedResource, blockingTaskTypes)
            => TaskType = taskType;

        public virtual Guid Identifier { get; } = Guid.NewGuid();

        public virtual bool ShouldBlockExit { get; set; } = true;

        public virtual bool IsExclusive { get; set; }

        public virtual TaskType TaskType { get; } = TaskType.Undefined;

        public virtual Task InnerTask { get; set; }

        public virtual void Start() => InnerTask.Start();

        public virtual void Wait(CancellationToken token) => InnerTask.Wait(token);

        public virtual SharpEncryptTaskResultModel Result { get; set; } = new SharpEncryptTaskResultModel();
    }
}