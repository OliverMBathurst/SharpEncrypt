using SharpEncrypt.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SharpEncrypt.Models
{
    public class ResourceBlockerModel
    {
        protected ResourceBlockerModel(ResourceType resourceType, params string[] blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources.ToList();
        }

        protected ResourceBlockerModel(ResourceType resourceType, IEnumerable<string> blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources.ToList();
        }

        protected ResourceBlockerModel(ResourceType resourceType, IEnumerable<string> blockedResources, params TaskType[] taskTypes)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources.ToList();
            BlockingTaskTypes = taskTypes.ToList();
        }

        protected ResourceBlockerModel(ResourceType resourceType, string blockedResource, params TaskType[] taskTypes)
        {
            ResourceType = resourceType;
            BlockedResources = new List<string> { blockedResource };
            BlockingTaskTypes = taskTypes.ToList();
        }

        public virtual ResourceType ResourceType { get; }

        public virtual List<string> BlockedResources { get; }

        public virtual List<TaskType> BlockingTaskTypes { get; }
    }
}
