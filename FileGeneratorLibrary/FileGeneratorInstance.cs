using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileGeneratorLibrary
{
    public sealed class FileGeneratorInstance
    {
        public void CreateDummyFile(string filePath, long length)
        {
            using (var fs = File.Create(filePath))
            {
                fs.SetLength(length);
            }
        }

        public void WriteNewFile(string path, long length = -1L, bool random = true, bool postDelete = true)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory does not exist");

            var drive = DriveInfo.GetDrives().First(x => char.ToLower(x.Name[0]) == char.ToLower(path[0]));

            if (length == -1L)
                length = drive.AvailableFreeSpace;

            var genFilePath = CreateUniqueFileForDirectory(path, ".BIN");

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var sw = new BinaryWriter(File.Open(genFilePath, FileMode.Open)))
                {
                    var completed = false;
                    while (!completed)
                    {
                        var writeSize = 1024L;
                        if (length < writeSize)
                            writeSize = length;

                        var bytes = new byte[writeSize];
                        if (random)
                            provider.GetNonZeroBytes(bytes);
                        sw.Write(bytes);

                        length -= writeSize;

                        if (length == 0L)
                            completed = true;
                    }
                }
            }

            if (postDelete)
                File.Delete(genFilePath);
        }

        public string CreateUniqueFileForDirectory(string directoryPath, string extension)
        {
            var uniqueFilePath = CreateUniqueFilePathForDirectory(directoryPath, extension);
            File.Create(uniqueFilePath);
            return uniqueFilePath;
        }

        public string CreateUniqueFilePathForDirectory(string directoryPath, string extension)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException("dirPath");
            if (!Directory.Exists(directoryPath))
                throw new IOException($"{directoryPath} is not a valid directory");

            var dir = Path.GetDirectoryName(directoryPath);
            var path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            while (File.Exists(path))
                path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            return path;
        }

        public string GetRandomNameWithoutExtension()
            => Path.GetRandomFileName().Split('.')[0];

        public string GetRandomExtension()
            => Path.GetRandomFileName().Split('.')[1];

        public string GetRandomNameWithExtension()
            => Path.GetRandomFileName();

        ///<summary>
        ///<c>GetAnonymousName()</c> should not be used to generate names for the renaming of sensitive files.
        ///The file name string that is returned by this method leaks metadata (the epoch time at which the file name was generated), use <c>GetRandomName()</c> instead.
        ///<summary>
        public string GetAnonymousName()
            => Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString();

        public void WriteNewFile(DriveInfo driveInfo, long size = -1L, bool random = true, bool postDelete = true) => WriteNewFile(driveInfo.RootDirectory.FullName, size, random, postDelete);
    }
}
