using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class EncryptTempFoldersTaskResultModel : FinalizableTaskModel
    {
        public List<FolderModel> ContainerizedFolders { get; set; }

        public List<FolderModel> UncontainerizedFolders { get; set; }
    }
}
