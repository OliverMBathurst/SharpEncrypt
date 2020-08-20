using SharpEncrypt.AbstractClasses;
using System.Threading;

namespace SharpEncrypt.Models
{
    internal sealed class CurrentTaskInstanceModel
    {
        public SharpEncryptTask Task { get; set; }

        public CancellationTokenSource Source { get; set; }
    }
}
