using SecureEraseLibrary;
using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FileModel : IEquatable<FileModel>
    {
        public string File { get; set; }

        public DateTime Time { get; set; }

        public string Secured { get; set; }

        public CipherType Algorithm { get; set; }

        public bool Equals(FileModel other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return Secured.Equals(other.Secured, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is FileModel model && Equals(model);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}