using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class OpenAesPasswordStoreTaskResultModel
    {
        public OpenAesPasswordStoreTaskResultModel(List<PasswordModel> models) => Models = models;

        public List<PasswordModel> Models { get; set; }
    }
}
