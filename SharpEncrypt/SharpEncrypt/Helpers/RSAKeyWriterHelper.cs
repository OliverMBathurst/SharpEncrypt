using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace SharpEncrypt.Helpers
{
    internal static class RSAKeyWriterHelper
    {
        public static void Write(string path, RSAParameters parameters)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException(dir);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, parameters);
            }
        }
    }
}
