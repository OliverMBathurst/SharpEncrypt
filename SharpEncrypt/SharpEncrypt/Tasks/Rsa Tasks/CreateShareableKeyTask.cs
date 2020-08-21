using System.Security.Cryptography;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Rsa_Tasks
{
    internal sealed class CreateShareableKeyTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.CreateShareableKeyTask;

        public CreateShareableKeyTask(RSAParameters privateKey, string filePath, string password) : base(ResourceType.File, filePath) =>
            InnerTask = new Task(() =>
            {
                RsaKeyWriterHelper.SerializeTextToFile(privateKey, password, filePath);
            });
    }
}