using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class FolderModel
    {
        public string Uri { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public List<FileModel> FileModels { get; set; } = new List<FileModel>();

        public override bool Equals(object obj) => obj is FolderModel model && Equals(model);

        public bool Equals(FolderModel other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return Uri.Equals(other.Uri, StringComparison.Ordinal)
                   && Time.Ticks.Equals(other.Time.Ticks);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
