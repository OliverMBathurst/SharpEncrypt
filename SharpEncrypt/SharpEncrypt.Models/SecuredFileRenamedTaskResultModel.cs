using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class SecuredFileRenamedTaskResultModel
    {
        public string OldPath { get; set; }

        public string NewPath { get; set; }
    }
}
