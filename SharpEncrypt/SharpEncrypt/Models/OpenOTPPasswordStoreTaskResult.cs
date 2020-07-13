using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class OpenOTPPasswordStoreTaskResult
    {
        public OpenOTPPasswordStoreTaskResult(List<PasswordModel> models, string storePath, string keyPath)
        {
            Models = models;
            StorePath = storePath;
            KeyPath = keyPath;
        }

        public List<PasswordModel> Models { get; }

        public string StorePath { get; }

        public string KeyPath { get; }
    }
}
