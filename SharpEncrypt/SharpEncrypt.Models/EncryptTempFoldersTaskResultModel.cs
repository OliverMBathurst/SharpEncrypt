using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class EncryptTempFoldersTaskResultModel : FinalizableTaskModel
    {
        public List<FolderModel> ContainerizedFolders { get; set; }

        public List<FolderModel> UncontainerizedFolders { get; set; }
    }
}
