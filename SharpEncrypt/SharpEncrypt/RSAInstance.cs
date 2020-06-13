using System.Security.Cryptography;

namespace SharpEncrypt
{
    internal sealed class RSAInstance
    {
        public (RSAParameters publicKey, RSAParameters privateKey) GetNewKeyPair()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                return (rsa.ExportParameters(false), rsa.ExportParameters(true));
            }
        }

        public RSAParameters GetNewKeyPairParameters()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                return rsa.ExportParameters(true);
            }
        }
    }
}
