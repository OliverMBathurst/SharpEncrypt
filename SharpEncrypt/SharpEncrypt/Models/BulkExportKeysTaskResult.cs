using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class BulkExportKeysTaskResult
    {
        public string KeyPath { get; set; }

        public List<string> NotCreated { get; set; }
    }
}
