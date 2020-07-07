using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OTPSaveKeyOfFileTask : SharpEncryptTask
    {
        public override bool IsSpecial => false;

        public override TaskType TaskType => TaskType.OTPSaveKeyOfFileTask;

        public OTPSaveKeyOfFileTask(string keyPath, string filePath) : base(ResourceType.File, keyPath, filePath)
        {
            InnerTask = new Task(() =>
            {
                OTPHelper.GenerateKey(keyPath, filePath);
            });
        }
    }
}
