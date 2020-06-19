using AESLibrary;
using SecureEraseLibrary;
using System;
using System.IO;
using System.Linq;

namespace SharpEncrypt
{
    internal static class ContainerHelper
    {
        public static bool ValidateContainer(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var guidBytes = Constants.GetGuidBytes();
                if (fs.Length < guidBytes.Length * 2 + Constants.KeySize)
                    return false;

                var buffer = new byte[guidBytes.Length];
                fs.Read(buffer, 0, buffer.Length);

                if (!guidBytes.SequenceEqual(buffer))
                    return false;

                buffer = new byte[Constants.KeySize];
                fs.Read(buffer, 0, buffer.Length);

                buffer = new byte[guidBytes.Length];
                fs.Read(buffer, 0, buffer.Length);

                if (!guidBytes.SequenceEqual(buffer))
                    return false;                
            }

            return true;
        }

        public static void Containerize(string filePath, byte[] encryptedKeyBytes, int bufferLength = 1024)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var destFilePath = $"{Path.GetFileName(filePath)}.seef";

            while (File.Exists(destFilePath))
                destFilePath += ".seef";

            using (var fs = new FileStream(destFilePath, FileMode.Create))
            {
                var guidBytes = Constants.GetGuidBytes();
                fs.Write(guidBytes, 0, guidBytes.Length);
                fs.Write(encryptedKeyBytes, 0, encryptedKeyBytes.Length);
                fs.Write(guidBytes, 0, guidBytes.Length);

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
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            AESHelper.EncryptFile(key, filePath);

            var keyBytes = AESHelper.PasswordEncrypt(key, password);
            Containerize(filePath, keyBytes);
        }
    }
}
