using SharpEncrypt.AbstractClasses;
using System.Threading;

namespace SharpEncrypt.Models
{
    public sealed class CurrentTaskInstance
    {
        public SharpEncryptTask Task { get; set; }

        public CancellationTokenSource Token { get; set; }
    }
}
