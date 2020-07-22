using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    internal sealed class OtpSaveKeyOfFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OtpSaveKeyOfFileTask;

        public OtpSaveKeyOfFileTask(string keyPath, string filePath) : base(ResourceType.File, keyPath, filePath)
        {
            InnerTask = new Task(() =>
            {
                OtpHelper.GenerateKey(keyPath, filePath);
            });
        }
    }
}
