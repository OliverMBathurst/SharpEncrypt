using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class FileModel : IEquatable<FileModel>
    {
        public string File { get; set; }

        public DateTime Time { get; set; }

        public string Secured { get; set; }

        public string Algorithm => Constants.DefaultEncryptionStandard;

        public bool Equals(FileModel other) => other != null && Secured.Equals(other.Secured, StringComparison.Ordinal);

        public override bool Equals(object obj) => obj is FileModel model && Equals(model);

        public override int GetHashCode() => base.GetHashCode();
    }
}