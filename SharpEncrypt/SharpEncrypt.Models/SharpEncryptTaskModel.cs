using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Models
{
    public class SharpEncryptTaskModel : ResourceBlockerModel
    {
        protected SharpEncryptTaskModel(ResourceType resourceType, TaskType taskType, params string[] blockedResources) :
            base(resourceType, blockedResources)
            => TaskType = taskType;

        protected SharpEncryptTaskModel(ResourceType resourceType, params string[] blockedResources) : base(resourceType, blockedResources) { }

        protected SharpEncryptTaskModel(ResourceType resourceType, IEnumerable<string> blockedResources) : base(resourceType, blockedResources) { }

        protected SharpEncryptTaskModel(ResourceType resourceType, IEnumerable<string> blockedResources,
            params TaskType[] blockingTaskTypes) : base(resourceType, blockedResources, blockingTaskTypes) { }

        protected SharpEncryptTaskModel(ResourceType resourceType, string blockedResource, TaskType taskType,
            params TaskType[] blockingTaskTypes) : base(resourceType, blockedResource, blockingTaskTypes)
            => TaskType = taskType;

        public virtual Guid Identifier { get; } = Guid.NewGuid();

        public virtual bool ShouldBlockExit { get; set; } = true;

        public virtual bool IsExclusive { get; set; }

        public virtual TaskType TaskType { get; } = TaskType.UndefinedTask;

        public virtual Task InnerTask { get; set; }

        public virtual void Start() => InnerTask.Start();

        public virtual void Wait(CancellationToken token) => InnerTask.Wait(token);

        public virtual Delegate AfterCompletion { get; set; }

        public virtual SharpEncryptTaskResultModel Result { get; set; } = new SharpEncryptTaskResultModel();
    }
}