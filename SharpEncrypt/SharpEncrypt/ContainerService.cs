using AESLibrary;
using SecureEraseLibrary;
using System.IO;
using System.Text;

namespace SharpEncrypt
{
    internal sealed class ContainerService
    {
        private readonly AESInstance aes = new AESInstance();

        public void Containerize(string filePath, byte[] encryptedKeyBytes, int bufferLength = 1024)
        {
            var destFilePath = $"{Path.GetFileName(filePath)}.seef";

            while (File.Exists(destFilePath))
                destFilePath += ".seef";

            using (var fs = new FileStream(destFilePath, FileMode.Create))
            {
                var guidBytes = Encoding.ASCII.GetBytes(Constants.GuidIdentifier);
                fs.Write(guidBytes, 0, guidBytes.Length);
                fs.Write(encryptedKeyBytes, 0, encryptedKeyBytes.Length);

                using (var ifs = new FileStream(filePath, FileMode.Open))
                {
                    var buffer = new byte[bufferLength];
                    int read;
                    while ((read = ifs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, read);
                    }
                }
            }

            new SecureEraseInstance().SDeleteFileWipe(filePath);
        }

        public void Containerize(string filePath, AESKey key, string password)
        {
            aes.EncryptFile(key, filePath);

            var keyBytes = aes.PasswordEncrypt(key, password);
            Containerize(filePath, keyBytes);
        }
    }
}
