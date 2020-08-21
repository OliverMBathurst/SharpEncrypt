using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class DecontainerizeFolderFilesTaskResultModel
    {
        public FolderModel Model { get; set; }

        public bool RemoveAfter { get; set; }

        public bool Temporary { get; set; }
    }
}
