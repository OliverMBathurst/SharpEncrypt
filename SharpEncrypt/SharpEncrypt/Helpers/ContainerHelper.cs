using AESLibrary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using SharpEncrypt.Exceptions;

namespace SharpEncrypt.Helpers
{
    internal static class ContainerHelper
    {
        private const int KEY_SIZE = 256, BLOCK_SIZE = 128, AES_KEY_LENGTH = 544, SALT_LENGTH = 32;

        public static bool ValidateContainer(string filePath, string password)
            => GetDecryptedAESKey(filePath, password) != null;

        public static void DecontainerizeFile(string filePath, string password)
        {
            var key = GetDecryptedAESKey(filePath, password);

            if (key != null)
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    fs.SetLength(fs.Length - (AES_KEY_LENGTH + SALT_LENGTH));
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
                            using (var fs = new FileStream(filePath, FileMode.Append))
                            {
                                fs.Write(encBytes, 0, encBytes.Length);
                                fs.Write(salt, 0, salt.Length);
                            }
                        }
                    }
                }
            }
        }

        private static AESKey GetDecryptedAESKey(string filePath, string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var salt = new byte[SALT_LENGTH];
            var decryptedKeyBytes = new byte[AES_KEY_LENGTH];

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                if (fs.Length < (AES_KEY_LENGTH + SALT_LENGTH))
                    throw new InvalidEncryptedFileException();

                fs.Seek(fs.Length - SALT_LENGTH, SeekOrigin.Begin);
                fs.Read(salt, 0, salt.Length);

                using (var AES = new RijndaelManaged())
                {
                    using (var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000, HashAlgorithmName.SHA512))
                    {
                        AES.KeySize = KEY_SIZE;
                        AES.BlockSize = BLOCK_SIZE;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        var encKeyBytes = new byte[AES_KEY_LENGTH];
                        fs.Seek(fs.Length - (AES_KEY_LENGTH + SALT_LENGTH), SeekOrigin.Begin);
                        fs.Read(encKeyBytes, 0, encKeyBytes.Length);

                        using (var ms = new MemoryStream(encKeyBytes))
                        {
                            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Read))
                            {
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