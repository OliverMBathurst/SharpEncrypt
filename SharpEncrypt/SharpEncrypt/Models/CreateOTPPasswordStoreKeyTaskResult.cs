using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class CreateOTPPasswordStoreKeyTaskResult
    {
        public string StorePath { get; set; }

        public string KeyPath { get; set; }

        public bool OpenAfter { get; set; }
    }
}
