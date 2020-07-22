using FileIOLibrary;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using AESLibrary;
using AesLibrary.Exceptions;

namespace AesLibrary
{
    public static class AesHelper
    {
        private const int BufferLength = 1024;

        /// <summary>
        ///
        /// </summary>
        public static bool TryGetKey(string path, out AesKey key)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var obj = new BinaryFormatter().Deserialize(fs);
                if (obj is AesKey aesKey)
                {
                    key = aesKey;
                    return true;
                }

                key = null;
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void EncryptFile(AesKey aesKey, string inputPath, string outputPath, int bufferLength = BufferLength)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIv();
            var keyLength = aesKey.KeySize;

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
                rif.BlockSize = aesKey.BlockSize;
                rif.Mode = aesKey.Mode;
                rif.Padding = aesKey.Padding;

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

        /// <summary>
        ///
        /// </summary>
        public static void DecryptFile(AesKey aesKey, string inputPath, string outputPath, int bufferLength = BufferLength)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIv();
            var keyLength = aesKey.KeySize;

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
                rif.BlockSize = aesKey.BlockSize;
                rif.Mode = aesKey.Mode;
                rif.Padding = aesKey.Padding;

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

        /// <summary>
        ///
        /// </summary>
        public static void DecryptFile(AesKey aesKey, string filePath, int bufferLength = BufferLength)
        {
            if (aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIv();
            var keyLength = aesKey.KeySize;

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
                rif.BlockSize = aesKey.BlockSize;
                rif.Mode = aesKey.Mode;
                rif.Padding = aesKey.Padding;

                using (var decryptor = rif.CreateDecryptor(key, iv))
                {
                    var bytes = new byte[bufferLength];
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read();
                        using (var ms = new MemoryStream(readWriter.GetBuffer()))
                        {
                            if (ms.Length < bytes.Length)
                                bytes = new byte[ms.Length];

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

        /// <summary>
        ///
        /// </summary>
        public static void EncryptFile(AesKey aesKey, string filePath, int bufferLength = BufferLength)
        {
            if(aesKey == null)
                throw new ArgumentNullException(nameof(aesKey));

            var key = aesKey.GetKey();
            var iv = aesKey.GetIv();
            var keyLength = aesKey.KeySize;

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
                rif.BlockSize = aesKey.BlockSize;
                rif.Mode = aesKey.Mode;
                rif.Padding = aesKey.Padding;

                using (var encryptor = rif.CreateEncryptor(key, iv))
                {
                    var readWriter = new SynchronizedReadWriter(filePath);
                    while (!readWriter.WriteComplete)
                    {
                        readWriter.Read(bufferLength);
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

        /// <summary>
        ///
        /// </summary>
        public static AesKey WriteNewKey(string path)
        {
            var key = GetNewAesKey();
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                new BinaryFormatter().Serialize(stream, key);
            }
            return key;
        }

        /// <summary>
        ///
        /// </summary>
        public static AesKey WriteNewKey(string path, RijndaelManaged managed)
        {
            var key = new AesKey(managed);
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                new BinaryFormatter().Serialize(stream, key);
            }
            return key;
        }

        /// <summary>
        ///
        /// </summary>
        public static AesKey GetNewAesKey(
            int keySize = 256, 
            int blockSize = 128,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (var key = new RijndaelManaged())
            {
                key.Mode = cipherMode;
                key.Padding = paddingMode;
                key.KeySize = keySize;
                key.BlockSize = blockSize;

                key.GenerateKey();
                key.GenerateIV();
                return new AesKey(key);
            }
        }
    }
}