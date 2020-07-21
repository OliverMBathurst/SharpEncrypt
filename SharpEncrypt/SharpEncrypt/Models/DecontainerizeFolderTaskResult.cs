using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class DecontainerizeFolderTaskResult
    {
        public FolderDataGridItemModel Model { get; set; }

        public bool RemoveAfter { get; set; }
    }
}
