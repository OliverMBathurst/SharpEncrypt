using System;

namespace SharpEncrypt.Models
{
    public class SharpEncryptTaskResult
    {
        public virtual Type Type { get; set; }

        public virtual object Value { get; set; }

        public virtual Exception Exception { get; set; }
    }
}
