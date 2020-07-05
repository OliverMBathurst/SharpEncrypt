using SharpEncrypt.Enums;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class ResourceBlocker
    {
        private readonly string[] _blockedResources;
        
        public ResourceBlocker(ResourceType resourceType, params string[] blockedResources)
        {
            ResourceType = resourceType;
            _blockedResources = blockedResources;
        }

        public virtual ResourceType ResourceType { get; set; }

        public virtual string[] GetBlockedResources() => _blockedResources;
    }
}
