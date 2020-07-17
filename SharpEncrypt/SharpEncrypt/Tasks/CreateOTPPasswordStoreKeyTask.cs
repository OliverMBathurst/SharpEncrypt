using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class CreateOTPPasswordStoreKeyTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.CreateOTPPasswordStoreKeyTask;

        public override bool IsSpecial => false;

        public CreateOTPPasswordStoreKeyTask(string storeFilePath, string keyPath, bool open) : base(ResourceType.File, keyPath)
        {
            InnerTask = new Task(() =>
            {
                OTPHelper.GenerateKey(keyPath, 1024*1024);
                OTPHelper.Transform(storeFilePath, keyPath);

                Result.Value = new CreateOTPPasswordStoreKeyTaskResult { StorePath = storeFilePath, KeyPath = keyPath, OpenAfter = open };
            });
        }
    }
}