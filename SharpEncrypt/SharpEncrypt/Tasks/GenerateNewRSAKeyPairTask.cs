using AESLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class GenerateNewRsaKeyPairTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.GenerateNewRSAKeyPairTask;

        public GenerateNewRsaKeyPairTask((string @public, string @private) keyPaths, string sessionPassword) : base(ResourceType.File, keyPaths.@public, keyPaths.@private)
        {
            InnerTask = new Task(() =>
            {
                var (@public, @private) = keyPaths;

                var (publicKey, privateKey) = RsaKeyPairHelper.GetNewKeyPair();
                RsaKeyWriterHelper.Write(@public, publicKey);
                RsaKeyWriterHelper.Write(@private, privateKey);
                ContainerHelper.ContainerizeFile(@private, AESHelper.GetNewAESKey(), sessionPassword);
            });
        }
    }
}