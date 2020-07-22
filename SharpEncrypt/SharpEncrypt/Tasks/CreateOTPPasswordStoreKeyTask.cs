using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class CreateOtpPasswordStoreKeyTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.CreateOTPPasswordStoreKeyTask;

        public CreateOtpPasswordStoreKeyTask(string storeFilePath, string keyPath, bool open) : base(ResourceType.File, keyPath)
        {
            InnerTask = new Task(() =>
            {
                OTPHelper.GenerateKey(keyPath, 1024*1024);
                OTPHelper.Transform(storeFilePath, keyPath);

                Result.Value = new CreateOtpPasswordStoreKeyTaskResult { StorePath = storeFilePath, KeyPath = keyPath, OpenAfter = open };
            });
        }
    }
}