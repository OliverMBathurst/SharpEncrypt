using System;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class DecontainerizeFileTaskResultModel
    {
        public FileModel Model { get; set; }

        public string NewPath { get; set; }

        public bool DeleteAfter { get; set; }

        public bool OpenAfter { get; set; }
    }
}
