using FileIOLibrary;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace AESLibrary
{
    public static class AESHelper
    {
        private const long BUFFER_LENGTH = 1024L;

        public static bool TryGetKey(string path, out AESKey key)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var obj = new BinaryFormatter().Deserialize(fs);
                if (obj is AESKey aesKey)
                {
                    key = aesKey;
                    return true;
                }

                key = null;
                return false;
            }
        }

        public static void EncryptFile(AESKey aesKey, string inputPath, string outputPath, long bufferLength = BUFFER_LENGTH)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIV();
            var keyLength = aesKey.KeyLength;

            if (!key.Any() || keyLength <= 0)
                throw new KeyLengthException(Resources.key);
            if (iv == null)
                throw new ArgumentNullException(Resources.iv);
            if (!iv.Any())
                throw new ByteArrayLengthException(Resources.iv);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = key;
                rif.IV = iv;
                rif.KeySize = keyLength;

                using (var encryptor = rif.CreateEncryptor(key, iv))
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

        public static void DecryptFile(AESKey aesKey, string inputPath, string outputPath, long bufferLength = BUFFER_LENGTH)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIV();
            var keyLength = aesKey.KeyLength;

            if (!key.Any() || keyLength <= 0)
                throw new KeyLengthException(Resources.key);
            if (iv == null)
                throw new ArgumentNullException(Resources.iv);
            if (!iv.Any())
                throw new ByteArrayLengthException(Resources.iv);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = key;
                rif.IV = iv;
                rif.KeySize = keyLength;

                using (var decryptor = rif.CreateDecryptor(key, iv))
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

        public static void DecryptFile(AESKey aesKey, string filePath)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIV();
            var keyLength = aesKey.KeyLength;

            if (!key.Any() || keyLength <= 0)
                throw new KeyLengthException(Resources.key);
            if (iv == null)
                throw new ArgumentNullException(Resources.iv);
            if (!iv.Any())
                throw new ByteArrayLengthException(Resources.iv);

            var readWriter = new SynchronizedReadWriter(filePath);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = key;
                rif.IV = iv;
                rif.KeySize = keyLength;

                using (var decryptor = rif.CreateDecryptor(key, iv))
                {
                    var bytes = new byte[SynchronizedReadWriter.DefaultBufferLength];
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read();
                        using (var ms = new MemoryStream(readWriter.GetBuffer()))
                        {
                            if (readWriter.GetBuffer().Length < bytes.Length)
                                bytes = new byte[readWriter.GetBuffer().Length];

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

        public static void EncryptFile(AESKey aesKey, string filePath)
        {
            if(aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIV();
            var keyLength = aesKey.KeyLength;

            if (!key.Any() || keyLength <= 0)
                throw new KeyLengthException(Resources.key);
            if (iv == null)
                throw new ArgumentNullException(Resources.iv);
            if (!iv.Any())
                throw new ByteArrayLengthException(Resources.iv);

            var readWriter = new SynchronizedReadWriter(filePath);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = key;
                rif.IV = iv;
                rif.KeySize = keyLength;

                using (var encryptor = rif.CreateEncryptor(key, iv))
                {
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read();
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                cs.Write(readWriter.GetBuffer(), 0, readWriter.GetBuffer().Length);
                            }
                            readWriter.SetBuffer(ms.ToArray());
                            readWriter.Write();
                        }
                    }
                }
            }
        }

        public static byte[] PasswordEncrypt(AESKey key, string password)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, key);
                var bytes = ms.ToArray();
                //encrypt bytes with pw and return
            }
            return Array.Empty<byte>();
        }

        public static AESKey WriteNewKey(string path)
        {
            var key = GetNewAESKey();
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                new BinaryFormatter().Serialize(stream, key);
            }
            return key;
        }

        public static AESKey GetNewAESKey()
        {
            using (var key = GenerateKey())
                return new AESKey(key);
        }

        private static RijndaelManaged GenerateKey()
        {
            using (var key = new RijndaelManaged())
            {
                key.GenerateKey();
                key.GenerateIV();
                return key;
            }            
        }
    }
}