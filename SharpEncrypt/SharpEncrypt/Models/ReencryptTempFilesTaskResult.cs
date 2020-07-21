using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class ReencryptTempFilesTaskResult
    {
        public List<string> UncontainerizedFiles { get; set; }

        public bool ExitAfter { get; set; }
    }
}
