using SharpEncrypt.AbstractClasses;
using System.Threading;

namespace SharpEncrypt.Models
{
    public sealed class CurrentTaskInstanceModel
    {
        public SharpEncryptTask Task { get; set; }

        public CancellationTokenSource Source { get; set; }
    }
}
