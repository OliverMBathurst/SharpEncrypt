using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace AESLibrary
{
    public sealed class AESInstance
    {
        private const long BUFFER_LENGTH = 1024L;

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

        public void EncryptFile(AESKey aesKey, string inputPath, string outputPath, long bufferLength = BUFFER_LENGTH)
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

                using (var encryptor = rif.CreateEncryptor(aesKey.Key, aesKey.IV))
                {
                    using (var outputStream = new FileStream(outputPath, FileMode.CreateNew))
                    {
                        using (var csEncrypt = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (var inputFileReader = new FileStream(inputPath, FileMode.Open))
                            {
                                var buffer = new byte[bufferLength];
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

        public void DecryptFile(AESKey aesKey, string inputPath, string outputPath, long bufferLength = BUFFER_LENGTH)
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

                using (var decryptor = rif.CreateDecryptor(aesKey.Key, aesKey.IV))
                {
                    using (var inStream = new FileStream(inputPath, FileMode.Open))
                    {
                        using (var csDecrypt = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var outFile = new FileStream(outputPath, FileMode.CreateNew))
                            {
                                var buffer = new byte[bufferLength];
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

        public void DecryptFile(AESKey aesKey, string filePath)
        {
            if (aesKey.Key == null || aesKey.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aesKey.IV == null || aesKey.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            var readWriter = new SynchronizedReadWriter(filePath);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = aesKey.Key;
                rif.IV = aesKey.IV;
                rif.KeySize = aesKey.KeySize;

                using (var decryptor = rif.CreateDecryptor(aesKey.Key, aesKey.IV))
                {
                    var bytes = new byte[readWriter.DefaultBufferLength];
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read();
                        using (var ms = new MemoryStream(readWriter.Buffer))
                        {
                            if (readWriter.Buffer.Length < bytes.Length)
                                bytes = new byte[readWriter.Buffer.Length];

                            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {                               
                                cs.Read(bytes, 0, bytes.Length);                                                
                            }
                            readWriter.SetBuffer(bytes);
                            readWriter.Write();
                        }
                    }
                }
            }
        }

        public void EncryptFile(AESKey aesKey, string filePath)
        {
            if (aesKey.Key == null || aesKey.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aesKey.IV == null || aesKey.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            var readWriter = new SynchronizedReadWriter(filePath);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = aesKey.Key;
                rif.IV = aesKey.IV;
                rif.KeySize = aesKey.KeySize;

                using (var encryptor = rif.CreateEncryptor(aesKey.Key, aesKey.IV))
                {
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read();
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                cs.Write(readWriter.Buffer, 0, readWriter.Buffer.Length);
                            }
                            readWriter.SetBuffer(ms.ToArray());
                            readWriter.Write();
                        }
                    }
                }
            }
        }

        public AESKey WriteNewKey(string path)
        {
            var key = GetNewAESKey();
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                new BinaryFormatter().Serialize(stream, key);
            }
            return key;
        }

        public AESKey GetNewAESKey()
            => new AESKey(GenerateKey());

        private RijndaelManaged GenerateKey()
        {
            var myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            return myRijndael;
        }
    }
}