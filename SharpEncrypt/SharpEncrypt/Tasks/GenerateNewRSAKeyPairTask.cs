using AESLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class GenerateNewRSAKeyPairTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.GenerateNewRSAKeyPairTask;

        public GenerateNewRSAKeyPairTask((string @public, string @private) keyPaths, string sessionPassword) 
            : base(ResourceType.File, keyPaths.@public, keyPaths.@private)
        {
            InnerTask = new Task(() =>
            {
                var (publicKey, privateKey) = RSAKeyPairHelper.GetNewKeyPair();
                RSAKeyWriterHelper.Write(keyPaths.@public, publicKey);
                RSAKeyWriterHelper.Write(keyPaths.@private, privateKey);
                ContainerHelper.ContainerizeFile(keyPaths.@private, AESHelper.GetNewAESKey(), sessionPassword);
            });
        }
    }
}