using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace SharpEncrypt
{
    internal sealed class RSAKeyWriter
    {
        public void Write(string path, RSAParameters parameters)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                throw new ArgumentException("path");

            using (var fs = new FileStream(path, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, parameters);
            }
        }
    }
}
