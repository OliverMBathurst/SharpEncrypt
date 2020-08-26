using System.Security.Cryptography;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Rsa_Tasks
{
    public sealed class CreateShareableKeyTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.CreateShareableKeyTask;

        public CreateShareableKeyTask(RSAParameters recipientPublicKey, string filePath, string password) : base(ResourceType.File, filePath) =>
            InnerTask = new Task(() =>
            {
                RsaKeyWriterHelper.SerializeTextToFile(recipientPublicKey, password, filePath);
            });
    }
}