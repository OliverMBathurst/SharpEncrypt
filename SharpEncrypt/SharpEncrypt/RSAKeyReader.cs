using SharpEncrypt.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace SharpEncrypt
{
    internal sealed class RSAKeyReader
    {
        public RSAParameters GetParameters(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var fs = new FileStream(path, FileMode.Open))
            {
                if(new BinaryFormatter().Deserialize(fs) is RSAParameters @params)
                {
                    return @params;
                }

                throw new InvalidKeyException(path);
            }
        }
    }
}
