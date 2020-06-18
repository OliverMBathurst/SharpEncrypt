using SecureEraseLibrary;
using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FileDataGridItemModel
    {
        public string File { get; set; }

        public long Time { get; set; }

        public string Secured { get; set; }

        public CipherType Algorithm { get; set; }
    }
}
