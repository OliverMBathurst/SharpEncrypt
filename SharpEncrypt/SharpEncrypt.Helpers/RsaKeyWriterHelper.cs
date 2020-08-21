using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace SharpEncrypt.Helpers
{
    public static class RsaKeyWriterHelper
    {
        public static void Write(string path, RSAParameters parameters)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            var dir = DirectoryHelper.GetDirectoryPath(path);
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException(dir);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, parameters);
            }
        }

        public static void SerializeTextToFile(RSAParameters key, string text, string filePath)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(key);

                using (var ms = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(ms, text);

                    var passwordBytes = ms.ToArray();
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        new BinaryFormatter().Serialize(fs, passwordBytes);
                    }
                }
            }
        }
    }
}
