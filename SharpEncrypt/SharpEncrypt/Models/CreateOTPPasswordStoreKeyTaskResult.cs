using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class CreateOtpPasswordStoreKeyTaskResult
    {
        public string StorePath { get; set; }

        public string KeyPath { get; set; }

        public bool OpenAfter { get; set; }
    }
}
