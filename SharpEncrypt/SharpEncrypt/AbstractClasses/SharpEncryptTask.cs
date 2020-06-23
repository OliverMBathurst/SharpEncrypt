using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Threading.Tasks;

namespace SharpEncrypt.AbstractClasses
{
    public abstract class SharpEncryptTask
    {
        public virtual SharpEncryptTaskResult Result { get; }

        public virtual TaskType TaskType { get; }

        public virtual Task InnerTask { get; set; }
    }
}