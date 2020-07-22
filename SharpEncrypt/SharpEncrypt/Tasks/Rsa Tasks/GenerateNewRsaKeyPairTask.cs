using System.Threading.Tasks;
using AesLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;

namespace SharpEncrypt.Tasks.Rsa_Tasks
{
    internal sealed class GenerateNewRsaKeyPairTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.GenerateNewRsaKeyPairTask;

        public GenerateNewRsaKeyPairTask((string @public, string @private) keyPaths, string sessionPassword) : base(ResourceType.File, keyPaths.@public, keyPaths.@private)
        {
            InnerTask = new Task(() =>
            {
                var (@public, @private) = keyPaths;

                var (publicKey, privateKey) = RsaKeyPairHelper.GetNewKeyPair();
                RsaKeyWriterHelper.Write(@public, publicKey);
                RsaKeyWriterHelper.Write(@private, privateKey);
                ContainerHelper.ContainerizeFile(@private, AesHelper.GetNewAesKey(), sessionPassword);
            });
        }
    }
}