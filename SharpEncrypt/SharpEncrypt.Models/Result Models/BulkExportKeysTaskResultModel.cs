using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models.Result_Models
{
    [Serializable]
    public sealed class BulkExportKeysTaskResultModel
    {
        public string KeyPath { get; set; }

        public List<string> NotCreated { get; set; }
    }
}
