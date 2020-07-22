using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FolderDataGridItemModel : IEquatable<FolderDataGridItemModel>
    {
        public string Uri { get; set; }

        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FolderDataGridItemModel model && Equals(model);
        }

        public bool Equals(FolderDataGridItemModel other)
        {
            if(other == null)
                throw new ArgumentNullException(nameof(other));

            return Uri.Equals(other.Uri, StringComparison.Ordinal)
                && Time.Ticks.Equals(other.Time.Ticks);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
