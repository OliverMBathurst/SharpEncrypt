using System;
using System.IO;
using System.Security.Cryptography;

namespace SecureShred
{
    internal class Shredder
    {
        private readonly string[] arguments;
        public Shredder(string[] args) => arguments = args;

        public void Shred()
        {
            if (arguments.Length == 1 && (arguments[0] == Resources.HelpSwitch || arguments[0] == Resources.HelpShortSwitch))
            {
                Console.WriteLine(Usage);
            }
            else
            {
                var path = string.Empty;
                var cipher = CipherType.OTP;
                var shredType = ShredType.File;
                bool recurse = false, nameObfuscation = true, propertyObfuscation = true;

                for (var i = 0; i + 1 < arguments.Length; i++)
                {
                    switch (arguments[i])
                    {
                        case "-path":
                            path = arguments[i + 1];
                            break;
                        case "-cipher" when Enum.TryParse(arguments[i + 1], out CipherType cipherResult):
                            cipher = cipherResult;
                            break;
                        case "-type" when Enum.TryParse(arguments[i + 1], out ShredType shredResult):
                            shredType = shredResult;
                            break;
                        case "-recurse" when bool.TryParse(arguments[i + 1], out var recurseResult):
                            recurse = recurseResult;
                            break;
                        case "-nameObfuscation" when bool.TryParse(arguments[i + 1], out var nameObfuscationResult):
                            nameObfuscation = nameObfuscationResult;
                            break;
                        case "-propertyObfuscation" when bool.TryParse(arguments[i + 1], out var propertyObfuscationResult):
                            propertyObfuscation = propertyObfuscationResult;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, arguments[i], Usage));
                    }
                    i++;
                }

                Shred(path, cipher, shredType, recurse, nameObfuscation, propertyObfuscation);
            }
        }


        private void Shred(string path, CipherType cipherType, ShredType shredType, bool recurse, bool nameObfuscation, bool propertyObfuscation)
        {
            if (shredType == ShredType.File)
            {
                ShredFile(path, cipherType, nameObfuscation, propertyObfuscation);
            }
            else
            {
                ShredDirectory(path, cipherType, recurse, nameObfuscation, propertyObfuscation);
            }
        }

        private void ShredDirectory(string path, CipherType cipherType, bool recurse, bool nameObfuscation, bool propertyObfuscation)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                throw new ArgumentException($"{path} is not a directory.");
            if (!Directory.Exists(path))
                throw new ArgumentException($"{path} does not exist.");

            var files = Directory.GetFiles(path);
            foreach (var file in files)
                ShredFile(file, cipherType, nameObfuscation, propertyObfuscation);

            if (recurse)
            {
                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                    ShredDirectory(directory, cipherType, recurse, nameObfuscation, propertyObfuscation);
            }

            if (propertyObfuscation)
                ObfuscateProperties(path, ShredType.Directory);

            if (nameObfuscation)
            {
                var result = ObfuscateName(path, ShredType.Directory);
                if (result != null)
                    path = result;
            }


            Directory.Delete(path);
        }

        private void ShredFile(string path, CipherType cipherType, bool nameObfuscation, bool propertyObfuscation)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                throw new ArgumentException($"{path} is not a file.");
            if (!new FileInfo(path).Exists)
                throw new ArgumentException($"{path} does not exist.");

            //TODO: Fix all this one individual projects have been completed, keys also should only be used once
            if (cipherType == CipherType.OTP)
            {
                using (var sr = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    using (var sw = new BinaryWriter(File.Open(path, FileMode.Open)))
                    {
                        using (var provider = new RNGCryptoServiceProvider())
                        {
                            var placeHolderByteArray = new byte[1];
                            while (sr.BaseStream.Position != sr.BaseStream.Length)
                            {
                                provider.GetBytes(placeHolderByteArray);
                                sw.Write(sr.ReadByte() ^ placeHolderByteArray[0]);
                            }
                        }
                    }
                }
            }
            else
            {
                using (var aes = new AesManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.GenerateIV();
                    aes.GenerateKey();

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        using (var fileCrypt = new FileStream(path, FileMode.Open))
                        {
                            using (var cs = new CryptoStream(fileCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (var fileInput = new FileStream(path, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fileInput.ReadByte()) != -1)
                                        cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }

            if (propertyObfuscation)
                ObfuscateProperties(path, ShredType.File);

            if (nameObfuscation)
                ObfuscateName(path, ShredType.File);

            File.Delete(path);
        }

        private void ObfuscateProperties(string path, ShredType shredType)
        {
            var unixTime = new DateTime(1970, 1, 1);
            if (shredType == ShredType.Directory)
            {
                Directory.SetCreationTime(path, unixTime);
                Directory.SetCreationTimeUtc(path, unixTime);
                Directory.SetLastAccessTime(path, unixTime);
                Directory.SetLastAccessTimeUtc(path, unixTime);
                Directory.SetLastWriteTime(path, unixTime);
                Directory.SetLastWriteTimeUtc(path, unixTime);
            }
            else
            {
                File.SetCreationTime(path, unixTime);
                File.SetCreationTimeUtc(path, unixTime);
                File.SetLastAccessTime(path, unixTime);
                File.SetLastAccessTimeUtc(path, unixTime);
                File.SetLastWriteTime(path, unixTime);
                File.SetLastWriteTimeUtc(path, unixTime);
            }
        }

        private string ObfuscateName(string path, ShredType shredType)
        {
            var rand = new Random();
            if (shredType == ShredType.Directory && Directory.Exists(path))
            {
                var parent = Directory.GetParent(path).FullName;
                var randName = GetRandName();
                while (Directory.Exists(Path.Combine(parent, randName)))
                    randName = GetRandName();

                Directory.Move(path, randName);
                return Path.Combine(parent, randName);
            }
            else if (shredType == ShredType.File && File.Exists(path))
            {
                var randomName = $"{GetRandName()}.ses";
                var currentDir = Path.GetDirectoryName(path);
                while (File.Exists(Path.Combine(currentDir, randomName)))
                    randomName = $"{GetRandName()}.ses";

                File.Move(path, randomName);
                return Path.Combine(currentDir, randomName);
            }

            string GetRandName()
            {
                var a = new int[5];
                for (var i = 0; i < 5; i++)
                    a[i] = rand.Next();
                return string.Join(string.Empty, a);
            }

            return null;
        }

        private enum CipherType { OTP, AES }

        private enum ShredType { File, Directory }

        private string Usage => Resources.Usage;
    }
}