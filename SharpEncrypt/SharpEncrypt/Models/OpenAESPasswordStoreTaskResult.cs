using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class OpenAesPasswordStoreTaskResult
    {
        public OpenAesPasswordStoreTaskResult(List<PasswordModel> models) => Models = models;

        public List<PasswordModel> Models { get; set; }
    }
}
