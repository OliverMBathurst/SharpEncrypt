using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class DecontainerizeFolderFilesTaskResult
    {
        public FolderModel Model { get; set; }

        public bool RemoveAfter { get; set; }

        public bool Temporary { get; set; }
    }
}
