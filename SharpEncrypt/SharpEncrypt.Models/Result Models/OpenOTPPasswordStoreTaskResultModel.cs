using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class OpenOtpPasswordStoreTaskResultModel
    {
        public OpenOtpPasswordStoreTaskResultModel() { }

        public OpenOtpPasswordStoreTaskResultModel(List<PasswordModel> models, string storePath, string keyPath)
        {
            Models = models;
            StorePath = storePath;
            KeyPath = keyPath;
        }

        public List<PasswordModel> Models { get; set; }

        public string StorePath { get; set; }

        public string KeyPath { get; set; }

        public Exception Exception { get; set; }
    }
}
