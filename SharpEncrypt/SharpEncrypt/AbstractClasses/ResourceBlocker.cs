using SharpEncrypt.Enums;
using System.Collections.Generic;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class ResourceBlocker
    {
        private readonly IEnumerable<string> _blockedResources;
        private readonly ResourceType _resourceType;
        
        public ResourceBlocker(ResourceType resourceType, params string[] blockedResources)
        {
            _resourceType = resourceType;
            _blockedResources = blockedResources;
        }

        public ResourceBlocker(ResourceType resourceType, IEnumerable<string> blockedResources)
        {
            _resourceType = resourceType;
            _blockedResources = blockedResources;
        }

        public virtual ResourceType ResourceType => _resourceType;

        public virtual IEnumerable<string> BlockedResources => _blockedResources;
    }
}
