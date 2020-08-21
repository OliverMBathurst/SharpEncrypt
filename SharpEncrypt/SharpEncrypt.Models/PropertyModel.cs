using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class PropertyModel
    {
        public string PropertyName { get; set; }

        public object PropertyValue { get; set; }
    }
}