using System;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class SecuredFileRenamedTaskResultModel
    {
        public string OldPath { get; set; }

        public string NewPath { get; set; }
    }
}
