using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    internal sealed class OtpSaveKeyOfFileTask : SharpEncryptTaskModel
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
