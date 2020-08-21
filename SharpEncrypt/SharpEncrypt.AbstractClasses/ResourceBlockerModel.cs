using SharpEncrypt.Enums;
using System.Collections.Generic;

namespace SharpEncrypt.AbstractClasses
{
    public class ResourceBlockerModel
    {
        protected ResourceBlockerModel(ResourceType resourceType, params string[] blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources;
        }

        protected ResourceBlockerModel(ResourceType resourceType, IEnumerable<string> blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources;
        }

        protected ResourceBlockerModel(ResourceType resourceType, IEnumerable<string> blockedResources, params TaskType[] taskTypes)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources;
            BlockingTaskTypes = taskTypes;
        }

        protected ResourceBlockerModel(ResourceType resourceType, string blockedResource, params TaskType[] taskTypes)
        {
            ResourceType = resourceType;
            BlockedResources = new [] { blockedResource };
            BlockingTaskTypes = taskTypes;
        }

        public virtual ResourceType ResourceType { get; }

        public virtual IEnumerable<string> BlockedResources { get; }

        public virtual IEnumerable<TaskType> BlockingTaskTypes { get; }
    }
}
