using System;

namespace SharpEncrypt.Models.Result_Models
{
    public class SharpEncryptTaskResultModel
    {
        public virtual Type Type { get; set; }

        public virtual object Value { get; set; }

        public virtual Exception Exception { get; set; }
    }
}
