using System;
using System.IO;

namespace SecureShred
{
    internal class SecureShred
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == Resources.HelpSwitch || args[0] == Resources.HelpShortSwitch))
            {
                Console.WriteLine(Usage);
            }
            else
            {
                var path = string.Empty;
                var cipher = CipherType.OTP;
                var shredType = ShredType.File;
                var recurse = false;

                for (var i = 0; i + 1 < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-path":
                            path = args[i + 1];
                            break;
                        case "-cipher" when Enum.TryParse(args[i + 1], out CipherType cipherResult):
                            cipher = cipherResult;
                            break;
                        case "-type" when Enum.TryParse(args[i + 1], out ShredType shredResult):
                            shredType = shredResult;
                            break;
                        case "-recurse" when bool.TryParse(args[i + 1], out var recurseResult):
                            recurse = recurseResult;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, args[i], Usage));
                    }
                    i++;
                }

                Shred(path, cipher, shredType, recurse);
            }
        }

        private static void Shred(string path, CipherType cipherType, ShredType shredType, bool recurse)
        {
            if(shredType == ShredType.File)
            {
                ShredFile(path, cipherType);
            }
            else
            {
                ShredDirectory(path, cipherType, recurse);
            }
        }

        private static void ShredDirectory(string path, CipherType cipherType, bool recurse)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                throw new ArgumentException($"{path} is not a directory.");
            if (!Directory.Exists(path))
                throw new ArgumentException($"{path} does not exist.");

            var files = Directory.GetFiles(path);
            foreach (var file in files)
                ShredFile(file, cipherType);

            if (recurse)
            {
                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                    ShredDirectory(directory, cipherType, recurse);
            }
        }

        private static void ShredFile(string path, CipherType cipherType)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                throw new ArgumentException($"{path} is not a file.");
            if (!new FileInfo(path).Exists)
                throw new ArgumentException($"{path} does not exist.");

            if(cipherType == CipherType.OTP)
            {
                using (var sr = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    using (var sw = new BinaryWriter(File.Open(path, FileMode.Open)))
                    {
                        var provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
                        var placeHolderByteArray = new byte[1];
                        while (sr.BaseStream.Position != sr.BaseStream.Length)
                        {
                            provider.GetBytes(placeHolderByteArray);
                            sw.Write(sr.ReadByte() ^ placeHolderByteArray[0]);                            
                        }
                    }
                }
            }
            else //AES logic here
            {

            }

            File.Delete(path);
        }

        private enum CipherType { OTP, AES }

        private enum ShredType { File, Directory }

        private static string Usage => Resources.Usage;
    }
}