using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class OnSecuredFileRenamedTaskResult
    {
        public string OldPath { get; set; }

        public string NewPath { get; set; }
    }
}
