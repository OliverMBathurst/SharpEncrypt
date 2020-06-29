using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OTPSaveKeyOfFileTask : SharpEncryptTask
    {
        public override bool IsLongRunning => true;

        public override TaskType TaskType => TaskType.OTPSaveKeyOfFileTask;

        public OTPSaveKeyOfFileTask(string keyPath, string filePath)
        {
            InnerTask = new Task(() =>
            {
                OTPHelper.GenerateKey(keyPath, filePath);
            });
        }
    }
}
