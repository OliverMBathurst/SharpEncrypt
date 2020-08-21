using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class FolderModel : IEquatable<FolderModel>
    {
        public string Uri { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public List<FolderModel> SubFolders { get; set; } = new List<FolderModel>();

        public List<FileModel> FileModels { get; set; } = new List<FileModel>();

        public override bool Equals(object obj) => obj is FolderModel model && Equals(model);

        public bool Equals(FolderModel other) => other != null && Uri.Equals(other.Uri, StringComparison.Ordinal) && Time.Ticks.Equals(other.Time.Ticks);

        public override int GetHashCode() => base.GetHashCode();
    }
}
