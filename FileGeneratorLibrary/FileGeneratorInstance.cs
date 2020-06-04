using SecureEraseLibrary;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileGeneratorLibrary
{
    public sealed class FileGeneratorInstance
    {
        private readonly SecureEraseInstance _secureEraseInstance = new SecureEraseInstance();

        public void WriteNewFile(string path, long size, bool random, bool postDelete)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!Directory.Exists(path))
                throw new ArgumentException(Resources.NotADir);

            var drive = DriveInfo.GetDrives().First(x => char.ToLower(x.Name[0]) == char.ToLower(path[0]));

            if (size == -1L)
                size = drive.AvailableFreeSpace;

            var genFilePath = _secureEraseInstance.CreateUniqueFileForDirectory(path, ".BIN");

            Console.WriteLine(string.Format(Resources.WritingTo, genFilePath));
            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var sw = new BinaryWriter(File.Open(genFilePath, FileMode.Open)))
                {
                    var completed = false;
                    while (!completed)
                    {
                        var writeSize = 1024L;
                        if (size < writeSize)
                            writeSize = size;

                        var bytes = new byte[writeSize];
                        if (random)
                            provider.GetNonZeroBytes(bytes);
                        sw.Write(bytes);

                        Console.WriteLine(string.Format(Resources.WroteNBytes, writeSize));

                        size -= writeSize;

                        if (size == 0L)
                            completed = true;
                    }
                }
            }

            if (postDelete)
                File.Delete(genFilePath);
        }
    }
}
