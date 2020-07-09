using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FolderDataGridItemModel : IEquatable<FolderDataGridItemModel>
    {
        public string URI { get; set; }

        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FolderDataGridItemModel model && Equals(model);
        }

        public bool Equals(FolderDataGridItemModel other)
        {
            return URI.Equals(other.URI, StringComparison.Ordinal)
                && Time.Ticks.Equals(other.Time.Ticks);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
