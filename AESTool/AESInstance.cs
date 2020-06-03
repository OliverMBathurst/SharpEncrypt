using ExtensionMethods.DateTimeExtensionMethods;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace AESTool
{
    internal class AESInstance
    {
        private readonly string[] arguments;

        public AESInstance(string[] args) => arguments = args;

        public void Execute()
        {
            if (arguments.Length == 1 && (arguments[0] == Resources.HelpSwitch || arguments[0] == Resources.HelpShortSwitch))
            {
                Console.WriteLine(Usage);
            }
            else
            {
                string path = string.Empty, keyPath = string.Empty;
                bool encrypt = true, genKey = false;
                int keySize = 256, blockSize = 128;

                for (var i = 0; i + 1 < arguments.Length; i++)
                {
                    switch (arguments[i])
                    {
                        case "-path":
                            path = arguments[i + 1];
                            i++;
                            break;
                        case "-encrypt":
                            encrypt = true;
                            break;
                        case "-decrypt":
                            encrypt = false;
                            break;
                        case "-key":
                            keyPath = arguments[i + 1];
                            i++;
                            break;
                        case "-genKey":
                            genKey = true;
                            break;
                        case "-keySize" when int.TryParse(arguments[i + 1], out var size):
                            keySize = size;
                            i++;
                            break;
                        case "-blockSize" when int.TryParse(arguments[i + 1], out var size):
                            blockSize = size;
                            i++;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, arguments[i], Usage));
                    }
                }

                if (genKey)
                    GenerateKey(path, keySize, blockSize);
                else
                    if (encrypt)
                        Encrypt(path, keyPath);
                    else
                        Decrypt(path, keyPath);
            }
        }

        private string Usage => Resources.Usage;

        private void GenerateKey(string path, int keySize, int blockSize)
        {
            using (var myRijndael = new RijndaelManaged())
            {                
                myRijndael.KeySize = keySize;
                myRijndael.BlockSize = blockSize;
                myRijndael.GenerateKey();
                myRijndael.GenerateIV();

                using (var stream = new FileStream(path, FileMode.Create))
                    new BinaryFormatter().Serialize(stream, new AESKey(myRijndael));
            }
        }

        [Serializable]
        private class AESKey
        {
            public AESKey(RijndaelManaged managed) 
            {
                Key = managed.Key;
                IV = managed.IV;
                KeySize = managed.KeySize;
                BlockSize = managed.BlockSize;
            }

            public byte[] Key { get; }

            public byte[] IV { get; }

            public int KeySize { get; }

            public int BlockSize { get; }
        }


        private void Encrypt(string path, string keyPath) => Transform(TransformType.Encrypt, path, keyPath);

        private void Decrypt(string path, string keyPath) => Transform(TransformType.Decrypt, path, keyPath);

        private void Transform(TransformType type, string path, string keyPath)
        {         
            var managed = (AESKey) new BinaryFormatter().Deserialize(new FileStream(keyPath, FileMode.Open));
            if(type == TransformType.Encrypt)
                Encrypt(managed, path);
            else
                Decrypt(managed, path);
        }

        private void Encrypt(AESKey managed, string path)
        {
            if (managed.Key == null || managed.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (managed.IV == null || managed.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            var fileName = Path.GetFileName(path);
            var ext = Path.GetExtension(path);
            var dir = Path.GetDirectoryName(path);

            var destFileName = $"{DateTime.Now.ToUnixTime()}{ext}";
            var outputPath = Path.Combine(dir, destFileName);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = managed.Key;
                rif.IV = managed.IV;
                rif.KeySize = managed.KeySize;
                rif.BlockSize = managed.BlockSize;

                using (var encryptor = rif.CreateEncryptor(managed.Key, managed.IV))
                {
                    using (var outputStream = new FileStream(outputPath, FileMode.CreateNew))
                    {
                        using (var csEncrypt = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (var inputFileReader = new FileStream(path, FileMode.Open))
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

            Erase(path);
            File.Move(outputPath, fileName);
        }

        private void Decrypt(AESKey managed, string path)
        {
            if (managed.Key == null || managed.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (managed.IV == null || managed.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            var fileName = Path.GetFileName(path);
            var ext = Path.GetExtension(path);
            var dir = Path.GetDirectoryName(path);

            var destFileName = $"{DateTime.Now.ToUnixTime()}{ext}";
            var outputPath = Path.Combine(dir, destFileName);

            using (var rif = new RijndaelManaged())
            {
                rif.Key = managed.Key;
                rif.IV = managed.IV;
                rif.KeySize = managed.KeySize;
                rif.BlockSize = managed.BlockSize;

                using (var decryptor = rif.CreateDecryptor(managed.Key, managed.IV))
                {
                    using (var inStream = new FileStream(path, FileMode.Open))
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

            Erase(path);
            File.Move(outputPath, fileName);
        }

        private void Erase(string path)
        {
            ObfuscateProperties(path);
            WriteRandomData(path);
            File.Delete(ObfuscatePath(path));
        }

        private void WriteRandomData(string path)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var byteArr = new byte[1024];
                    var fullLength = fs.Length;
                    while (fullLength > 0)
                    {
                        if (fullLength < 1024)
                            byteArr = new byte[fullLength];

                        provider.GetNonZeroBytes(byteArr);
                        fs.Write(byteArr, 0, byteArr.Length);
                        fullLength -= byteArr.Length;
                    }
                }
            }
        }

        private void ObfuscateProperties(string path)
        {
            var unixTime = new DateTime(1970, 1, 1);
            File.SetCreationTime(path, unixTime);
            File.SetCreationTimeUtc(path, unixTime);
            File.SetLastAccessTime(path, unixTime);
            File.SetLastAccessTimeUtc(path, unixTime);
            File.SetLastWriteTime(path, unixTime);
            File.SetLastWriteTimeUtc(path, unixTime);
        }

        private string ObfuscatePath(string path)
        {
            var currentPath = path;
            var dir = Path.GetDirectoryName(path);
            for (var i = 0; i < 8; i++)
            {
                var rand = Path.GetRandomFileName();
                try
                {
                    File.Move(currentPath, rand);
                    var newPath = Path.Combine(dir, rand);
                    currentPath = newPath;
                }catch(IOException)
                {
                    return currentPath;
                }
            }
            return currentPath;
        }

        private enum TransformType { Encrypt, Decrypt }
    }
}