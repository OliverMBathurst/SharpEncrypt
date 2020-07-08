using SecureEraseLibrary;
using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FileDataGridItemModel : IEquatable<FileDataGridItemModel>
    {
        public string File { get; set; }

        public DateTime Time { get; set; }

        public string Secured { get; set; }

        public CipherType Algorithm { get; set; }

        public bool Equals(FileDataGridItemModel other)
        {
            return Secured.Equals(other.Secured, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is FileDataGridItemModel model && Equals(model);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
