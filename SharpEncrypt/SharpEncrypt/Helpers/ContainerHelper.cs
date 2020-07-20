using AESLibrary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using SharpEncrypt.Exceptions;
using System.Linq;
using System.Collections.Generic;

namespace SharpEncrypt.Helpers
{
    internal static class ContainerHelper
    {
        private const int KEY_SIZE = 256, BLOCK_SIZE = 128, SALT_LENGTH = 32;

        public static bool ValidateContainer(string filePath, string password) => GetDecryptedAESKey(filePath, password, out _) != null;

        public static void DecontainerizeFile(string filePath, string password)
        {
            var key = GetDecryptedAESKey(filePath, password, out var keyLength);

            if (key != null)
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    fs.SetLength(fs.Length - (keyLength + SALT_LENGTH + GetLengthBytes(keyLength).Length + 1));
                }

                AESHelper.DecryptFile(key, filePath);
            }
            else
            {
                throw new InvalidEncryptedFileException();
            }      
        }

        public static void ContainerizeFile(string filePath, AESKey key, string password)
        {
            AESHelper.EncryptFile(key, filePath);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var salt = GenerateSalt();
            using (var passwordKey = new Rfc2898DeriveBytes(passwordBytes, salt, 50000, HashAlgorithmName.SHA512))
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = KEY_SIZE;
                    AES.BlockSize = BLOCK_SIZE;
                    AES.Key = passwordKey.GetBytes(AES.KeySize / 8);
                    AES.IV = passwordKey.GetBytes(AES.BlockSize / 8);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            var bytes = GetByteArray(key);
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();

                            var encBytes = ms.ToArray();
                            var lengthBytes = GetLengthBytes(encBytes.Length);
                            using (var fs = new FileStream(filePath, FileMode.Append))
                            {
                                fs.Write(encBytes, 0, encBytes.Length);
                                fs.Write(salt, 0, salt.Length);
                                fs.Write(lengthBytes, 0, lengthBytes.Length);
                                fs.WriteByte((byte)lengthBytes.Length);
                            }
                        }
                    }
                }
            }
        }

        private static AESKey GetDecryptedAESKey(string filePath, string password, out int keyLength)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] decryptedKeyBytes;

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                fs.Seek(fs.Length - 1, SeekOrigin.Begin);
                var lengthBytesLength = fs.ReadByte();

                fs.Seek(fs.Length - (1 + lengthBytesLength), SeekOrigin.Begin);
                var lengthBytes = new byte[lengthBytesLength];

                fs.Read(lengthBytes, 0, lengthBytes.Length);

                keyLength = lengthBytes.Sum(x => x);

                if (fs.Length < (keyLength + SALT_LENGTH + lengthBytesLength + 1))
                    throw new InvalidEncryptedFileException();

                var salt = new byte[SALT_LENGTH];
                fs.Seek(fs.Length - (lengthBytesLength + 1 + SALT_LENGTH), SeekOrigin.Begin);
                fs.Read(salt, 0, salt.Length);

                using (var AES = new RijndaelManaged())
                {
                    using (var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000, HashAlgorithmName.SHA512))
                    {
                        AES.KeySize = KEY_SIZE;
                        AES.BlockSize = BLOCK_SIZE;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        var encKeyBytes = new byte[keyLength];
                        fs.Seek(fs.Length - (keyLength + SALT_LENGTH + lengthBytesLength + 1), SeekOrigin.Begin);
                        fs.Read(encKeyBytes, 0, encKeyBytes.Length);

                        using (var ms = new MemoryStream(encKeyBytes))
                        {
                            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Read))
                            {
                                decryptedKeyBytes = new byte[ms.Length];
                                cs.Read(decryptedKeyBytes, 0, decryptedKeyBytes.Length);
                            }
                        }
                    }
                }
            }

            using (var ms = new MemoryStream(decryptedKeyBytes))
            {
                if (new BinaryFormatter().Deserialize(ms) is AESKey aesKey)
                {
                    return aesKey;
                }
            }

            return null;
        }

        private static byte[] GetLengthBytes(int number)
        {
            var list = new List<int>();
            while (number > 0)
            {
                if(number <= 255)
                {
                    list.Add(number);
                    number = 0;
                }
                else
                {
                    list.Add(255);
                    number -= 255;
                }
            }
            return list.Select(x => (byte)x).ToArray();
        }

        private static byte[] GetByteArray(object o)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, o);
                return ms.ToArray();
            }
        }

        private static byte[] GenerateSalt()
        {
            var data = new byte[SALT_LENGTH];
            using (var rgnCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rgnCryptoServiceProvider.GetBytes(data);
            }
            return data;
        }
    }
}