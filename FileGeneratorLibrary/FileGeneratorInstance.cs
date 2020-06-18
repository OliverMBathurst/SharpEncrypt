using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileGeneratorLibrary
{
    public sealed class FileGeneratorInstance
    {
        private const long BUFFER_LENGTH = 1024L;

        public static void CreateDummyFile(string filePath, long length)
        {
            using (var fs = File.Create(filePath))
            {
                fs.SetLength(length);
            }
        }

        public static void WriteNewFile(string path, long length = -1L, bool random = true, bool postDelete = true, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                throw new ArgumentNullException(nameof(path));
                        
            if (length == -1L)
            {
                var drive = DriveInfo.GetDrives().First(x => char.ToLower(x.Name[0], CultureInfo.CurrentCulture) == char.ToLower(path[0], CultureInfo.CurrentCulture));
                length = drive.AvailableFreeSpace;
            }

            var genFilePath = CreateUniqueFileForDirectory(path, ".BIN");

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var bw = new BinaryWriter(File.Open(genFilePath, FileMode.Open)))
                {
                    var buffer = new byte[bufferLength];
                    var remainingLength = length;

                    var completed = false;
                    while (!completed)
                    {
                        if (remainingLength < buffer.Length)
                            buffer = new byte[remainingLength];
                                                
                        if (random)
                            provider.GetNonZeroBytes(buffer);
                        bw.Write(buffer);

                        remainingLength -= buffer.Length;

                        if (remainingLength == 0L)
                            completed = true;
                    }
                }
            }

            if (postDelete)
                File.Delete(genFilePath);
        }

        public static string CreateUniqueFileForDirectory(string directoryPath, string extension)
        {
            var uniqueFilePath = CreateUniqueFilePathForDirectory(directoryPath, extension);
            using (var fs = File.Create(uniqueFilePath))
            {
                return uniqueFilePath;
            }
        }

        public static string CreateUniqueFilePathForDirectory(string directoryPath, string extension)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath));
            if (!Directory.Exists(directoryPath))
                throw new IOException($"{directoryPath} is not a valid directory.");

            var dir = Path.GetDirectoryName(directoryPath);
            var path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            while (File.Exists(path))
                path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            return path;
        }

        public static string GetRandomNameWithoutExtension() => Path.GetRandomFileName().Split('.')[0];

        public static string GetRandomExtension() => Path.GetRandomFileName().Split('.')[1];

        public static string GetRandomNameWithExtension() => Path.GetRandomFileName();

        ///<summary>
        ///<c>GetAnonymousName()</c> should not be used to generate names for the renaming of sensitive files.
        ///The file name string that is returned by this method leaks metadata (the epoch time at which the file name was generated), use <c>GetRandomName()</c> instead.
        ///<summary>
        public static string GetAnonymousName() => Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.CurrentCulture);

        public static void WriteNewFile(DriveInfo driveInfo, long size = -1L, bool random = true, bool postDelete = true)
        {
            if (driveInfo == null)
                throw new ArgumentNullException(nameof(driveInfo));
            WriteNewFile(driveInfo.RootDirectory.FullName, size, random, postDelete);
        }
    }
}
