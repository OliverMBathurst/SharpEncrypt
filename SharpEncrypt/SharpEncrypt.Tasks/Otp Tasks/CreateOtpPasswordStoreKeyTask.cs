using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    public sealed class CreateOtpPasswordStoreKeyTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.CreateOtpPasswordStoreKeyTask;

        public CreateOtpPasswordStoreKeyTask(string storeFilePath, string keyPath, bool open) : base(ResourceType.File, keyPath)
        {
            InnerTask = new Task(() =>
            {
                OtpHelper.GenerateKey(keyPath, 1024*1024);
                OtpHelper.Transform(storeFilePath, keyPath);

                Result.Value = new CreateOtpPasswordStoreKeyTaskResultModel { StorePath = storeFilePath, KeyPath = keyPath, OpenAfter = open };
            });
        }
    }
}