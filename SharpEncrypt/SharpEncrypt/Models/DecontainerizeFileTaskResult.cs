using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class DecontainerizeFileTaskResult
    {
        public FileDataGridItemModel Model { get; set; }

        public string NewPath { get; set; }

        public bool DeleteAfter { get; set; }

        public bool OpenAfter { get; set; }
    }
}
