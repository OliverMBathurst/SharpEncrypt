using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class SecuredFileRenamedTaskResultModel
    {
        public string OldPath { get; set; }

        public string NewPath { get; set; }
    }
}
