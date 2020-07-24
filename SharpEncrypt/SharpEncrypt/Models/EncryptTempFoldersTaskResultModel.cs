using System;
using System.Collections.Generic;
using SharpEncrypt.AbstractClasses;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class EncryptTempFoldersTaskResultModel : FinalizableTaskResult
    {
        public List<FolderModel> ContainerizedFolders { get; set; }

        public List<FolderModel> UncontainerizedFolders { get; set; }
    }
}
