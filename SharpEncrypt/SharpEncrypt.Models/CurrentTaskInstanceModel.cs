using System.Threading;

namespace SharpEncrypt.Models
{
    public sealed class CurrentTaskInstanceModel
    {
        public SharpEncryptTaskModel Task { get; set; }

        public CancellationTokenSource Source { get; set; }
    }
}
