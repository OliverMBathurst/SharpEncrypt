using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using AesLibrary;
using SharpEncrypt.Exceptions;

namespace SharpEncrypt.Helpers
{
    public static class RsaKeyReaderHelper
    {
        public static RSAParameters GetParameters(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var fs = new FileStream(path, FileMode.Open))
            {
                if (new BinaryFormatter().Deserialize(fs) is RSAParameters @params)
                {
                    return @params;
                }
            }

            throw new InvalidKeyException(path);
        }

        public static RSAParameters GetParameters(string path, string password)
        {
            ContainerHelper.DecontainerizeFile(path, password);

            RSAParameters key = default;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                if (new BinaryFormatter().Deserialize(fs) is RSAParameters parameters)
                {
                    key = parameters;
                }
            }
            ContainerHelper.ContainerizeFile(path, AesHelper.GetNewAesKey(), password);

            return key;
        }

        public static IDictionary<string, RSAParameters> GetPublicKeys(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                return new BinaryFormatter().Deserialize(fs) is IDictionary<string, RSAParameters> dict
                    ? dict
                    : new Dictionary<string, RSAParameters>();
            }                
        }
    }
}
