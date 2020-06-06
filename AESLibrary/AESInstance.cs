using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace AESLibrary
{
    public sealed class AESInstance
    {
        public RijndaelManaged GenerateKey(int keySize, int blockSize)
        {
            var myRijndael = new RijndaelManaged
            {
                KeySize = keySize,
                BlockSize = blockSize
            };
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            return myRijndael;
        }

        public bool TryGetKey(string path, out AESKey key)
        {
            var obj = new BinaryFormatter().Deserialize(new FileStream(path, FileMode.Open));
            if(obj is AESKey aesKey)
            {
                key = aesKey;
                return true;
            }

            key = null;
            return false;
        }

        public void Encrypt(AESKey aesKey, string inputPath, string outputPath)
        {
            if (aesKey.Key == null || aesKey.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aesKey.IV == null || aesKey.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (var rif = new RijndaelManaged())
            {
                rif.Key = aesKey.Key;
                rif.IV = aesKey.IV;
                rif.KeySize = aesKey.KeySize;
                rif.BlockSize = aesKey.BlockSize;

                using (var encryptor = rif.CreateEncryptor(aesKey.Key, aesKey.IV))
                {
                    using (var outputStream = new FileStream(outputPath, FileMode.CreateNew))
                    {
                        using (var csEncrypt = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (var inputFileReader = new FileStream(inputPath, FileMode.Open))
                            {
                                var buffer = new byte[1024];
                                int read;
                                while ((read = inputFileReader.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    csEncrypt.Write(buffer, 0, read);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Decrypt(AESKey aesKey, string inputPath, string outputPath)
        {
            if (aesKey.Key == null || aesKey.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aesKey.IV == null || aesKey.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (var rif = new RijndaelManaged())
            {
                rif.Key = aesKey.Key;
                rif.IV = aesKey.IV;
                rif.KeySize = aesKey.KeySize;
                rif.BlockSize = aesKey.BlockSize;

                using (var decryptor = rif.CreateDecryptor(aesKey.Key, aesKey.IV))
                {
                    using (var inStream = new FileStream(inputPath, FileMode.Open))
                    {
                        using (var csDecrypt = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var outFile = new FileStream(outputPath, FileMode.CreateNew))
                            {
                                var buffer = new byte[1024];
                                int read;
                                while ((read = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    outFile.Write(buffer, 0, read);
                                }
                            }
                        }
                    }
                }
            }
        }

        public AESKey WriteNewKey(string path, int keySize, int blockSize)
        {
            var key = GetNewAESKey(keySize, blockSize);
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                new BinaryFormatter().Serialize(stream, key);
            }
            return key;
        }

        public AESKey GetNewAESKey(int keySize, int blockSize)
            => new AESKey(GenerateKey(keySize, blockSize));
    }
}