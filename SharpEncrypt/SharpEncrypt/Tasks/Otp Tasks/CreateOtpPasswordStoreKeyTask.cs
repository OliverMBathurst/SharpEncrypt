using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    internal sealed class CreateOtpPasswordStoreKeyTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.CreateOtpPasswordStoreKeyTask;

        public CreateOtpPasswordStoreKeyTask(string storeFilePath, string keyPath, bool open) : base(ResourceType.File, keyPath)
        {
            InnerTask = new Task(() =>
            {
                OtpHelper.GenerateKey(keyPath, 1024*1024);
                OtpHelper.Transform(storeFilePath, keyPath);

                Result.Value = new CreateOtpPasswordStoreKeyTaskResult { StorePath = storeFilePath, KeyPath = keyPath, OpenAfter = open };
            });
        }
    }
}