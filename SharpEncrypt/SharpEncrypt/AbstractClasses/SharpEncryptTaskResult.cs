using System;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTaskResult
    {
        public Type Type { get; set; }

        public object Value { get; set; }

        public Exception Exception { get; set; }
    }
}
