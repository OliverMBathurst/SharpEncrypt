using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class KeyFileStoreFileTupleModel
    {
        public string StoreFile { get; set; }

        public string KeyFile { get; set; }
    }
}
