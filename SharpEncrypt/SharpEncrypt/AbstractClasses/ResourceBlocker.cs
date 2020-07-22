using SharpEncrypt.Enums;
using System.Collections.Generic;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class ResourceBlocker
    {
        protected ResourceBlocker(ResourceType resourceType, params string[] blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources;
        }

        protected ResourceBlocker(ResourceType resourceType, IEnumerable<string> blockedResources)
        {
            ResourceType = resourceType;
            BlockedResources = blockedResources;
        }

        public virtual ResourceType ResourceType { get; }

        public virtual IEnumerable<string> BlockedResources { get; }
    }
}
