using AESLibrary;
using SecureEraseLibrary;
using System.IO;
using System.Text;

namespace SharpEncrypt
{
    internal static class ContainerHelper
    {
        public static void Containerize(string filePath, byte[] encryptedKeyBytes, int bufferLength = 1024)
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

            SecureEraseHelper.SDeleteFileWipe(filePath);
        }

        public static void Containerize(string filePath, AESKey key, string password)
        {
            AESHelper.EncryptFile(key, filePath);

            var keyBytes = AESHelper.PasswordEncrypt(key, password);
            Containerize(filePath, keyBytes);
        }
    }
}
