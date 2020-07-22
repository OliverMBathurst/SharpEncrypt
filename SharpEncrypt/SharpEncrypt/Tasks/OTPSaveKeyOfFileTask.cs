using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OtpSaveKeyOfFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OTPSaveKeyOfFileTask;

        public OtpSaveKeyOfFileTask(string keyPath, string filePath) : base(ResourceType.File, keyPath, filePath)
        {
            InnerTask = new Task(() =>
            {
                OTPHelper.GenerateKey(keyPath, filePath);
            });
        }
    }
}
