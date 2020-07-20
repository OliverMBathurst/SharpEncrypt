using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class OpenAESPasswordStoreTaskResult
    {
        public OpenAESPasswordStoreTaskResult(List<PasswordModel> models) => Models = models;

        public List<PasswordModel> Models { get; set; }
    }
}
