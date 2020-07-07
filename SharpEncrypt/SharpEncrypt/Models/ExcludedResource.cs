using SharpEncrypt.Enums;
using System;
namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class ExcludedResource : IEquatable<ExcludedResource>
    {
        public ResourceType ResourceType { get; set; }

        public string URI { get; set; }

        public bool Equals(ExcludedResource other)
        {
            return URI == other.URI;
        }

        public override bool Equals(object obj)
        {
            return obj is ExcludedResource resource && Equals(resource);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
