using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTask : SharpEncryptTaskResult
    {
        public virtual TaskType TaskType { get; }

        public virtual Task InnerTask { get; set; }
    }
}