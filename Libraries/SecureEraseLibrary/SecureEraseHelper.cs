using AESLibrary;
using FileGeneratorLibrary;
using OTPLibrary;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SecureEraseLibrary
{
    /// <summary>
    ///
    /// </summary>
    public static class SecureEraseHelper
    {
        private readonly static char[] _alphabet = new []{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private const int BUFFER_LENGTH = 1024;

        /// <summary>
        ///
        /// </summary>
        public static void WriteZeros(string path, int passes = 1, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var byteArr = CreateByteArray(bufferLength, 0);
                for (var i = 0; i < passes; i++)
                {
                    fs.Seek(0, SeekOrigin.Begin);                    
                    var remainingLength = fs.Length;
                    while (remainingLength > 0)
                    {
                        if (remainingLength < byteArr.Length)
                            byteArr = CreateByteArray(remainingLength, 0);

                        fs.Write(byteArr, 0, byteArr.Length);
                        remainingLength -= byteArr.Length;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Write255s(string path, int passes = 1, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var byteArr = CreateByteArray(bufferLength, 255);
                    for (var i = 0; i < passes; i++)
                    {
                        fs.Seek(0, SeekOrigin.Begin);                        
                        var remainingLength = fs.Length;
                        while (remainingLength > 0)
                        {
                            if (remainingLength < byteArr.Length)
                                byteArr = CreateByteArray(remainingLength, 255);

                            fs.Write(byteArr, 0, byteArr.Length);
                            remainingLength -= byteArr.Length;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void WriteRandomData(string path, int passes = 1, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    for (var i = 0; i < passes; i++)
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        var byteArr = new byte[bufferLength];
                        var remainingLength = fs.Length;
                        while (remainingLength > 0)
                        {
                            if (remainingLength < bufferLength)
                                byteArr = new byte[remainingLength];

                            provider.GetNonZeroBytes(byteArr);
                            fs.Write(byteArr, 0, byteArr.Length);
                            remainingLength -= byteArr.Length;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void SDeleteDriveWipe(char driveLetter, string tempDirectory = "", bool postDelete = true, int passes = 1, int bufferLength = BUFFER_LENGTH)
        {
            var drives = DriveInfo.GetDrives().Where(x => char.ToLower(x.Name[0], CultureInfo.CurrentCulture) == char.ToLower(driveLetter, CultureInfo.CurrentCulture));
            if (!drives.Any())
                throw new DriveNotFoundException(driveLetter.ToString());

            var drive = drives.First();
            if (string.IsNullOrEmpty(tempDirectory))
                throw new ArgumentNullException(nameof(tempDirectory));
            if (!Directory.Exists(tempDirectory))
                throw new DirectoryNotFoundException(tempDirectory);
            if (char.ToLower(Path.GetPathRoot(tempDirectory)[0], CultureInfo.CurrentCulture) != char.ToLower(driveLetter, CultureInfo.CurrentCulture))
                throw new DirectoryDoesNotExistOnDriveException($"{tempDirectory} - {driveLetter}");
            
            var tmpFile = FileGeneratorHelper.CreateUniqueFilePathForDirectory(tempDirectory, FileGeneratorHelper.GetRandomExtension());
            FileGeneratorHelper.CreateDummyFile(tmpFile, drive.AvailableFreeSpace);
            WriteZeros(tmpFile, passes, bufferLength);
            Write255s(tmpFile, passes, bufferLength);
            WriteRandomData(tmpFile, passes, bufferLength);
            if (postDelete)
                File.Delete(tmpFile);
        }

        /// <summary>
        ///
        /// </summary>
        public static void SDeleteFileWipe(string filePath, bool postDelete = true, int bufferLength = BUFFER_LENGTH)
        {
            WriteZeros(filePath, bufferLength: bufferLength);
            Write255s(filePath, bufferLength: bufferLength);
            WriteRandomData(filePath, bufferLength: bufferLength);
            filePath = SDeleteFileRename(filePath);
            if (postDelete)
                File.Delete(filePath);
        }

        /// <summary>
        ///
        /// </summary>
        public static string SDeleteFileRename(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var split = Path.GetFileName(path).Split('.');
            var fileNameLength = split[0].Length;
            var extensionLength = split[1].Length;
            var dir = Path.GetDirectoryName(path);

            foreach(var character in _alphabet)
            {
                var newName = $"{Enumerable.Repeat(character, fileNameLength)}.{Enumerable.Repeat(character, extensionLength)}";
                var newPath = Path.Combine(dir, newName);
                if (File.Exists(newPath))
                {
                    continue;
                }
                else
                {
                    File.Move(path, newName);
                    path = newPath;
                }
            }

            return path;
        }

        /// <summary>
        ///
        /// </summary>
        public static void ObfuscateFileProperties(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var unixTime = new DateTime(1970, 1, 1);
            File.SetCreationTime(path, unixTime);
            File.SetCreationTimeUtc(path, unixTime);
            File.SetLastAccessTime(path, unixTime);
            File.SetLastAccessTimeUtc(path, unixTime);
            File.SetLastWriteTime(path, unixTime);
            File.SetLastWriteTimeUtc(path, unixTime);
        }

        /// <summary>
        ///
        /// </summary>
        public static void ObfuscateDirectoryProperties(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var unixTime = new DateTime(1970, 1, 1);
            Directory.SetCreationTime(path, unixTime);
            Directory.SetCreationTimeUtc(path, unixTime);
            Directory.SetLastAccessTime(path, unixTime);
            Directory.SetLastAccessTimeUtc(path, unixTime);
            Directory.SetLastWriteTime(path, unixTime);
            Directory.SetLastWriteTimeUtc(path, unixTime);
        }

        /// <summary>
        ///
        /// </summary>
        public static string ObfuscateFileName(string path, int passes = 8)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var currentPath = path;
            var dir = Path.GetDirectoryName(path);
            for (var i = 0; i < passes; i++)
            {
                var rand = FileGeneratorHelper.GetRandomNameWithoutExtension();
                while (File.Exists(Path.Combine(dir, rand)))
                    rand = FileGeneratorHelper.GetRandomNameWithoutExtension();

                File.Move(currentPath, rand);
                currentPath = Path.Combine(dir, rand);
            }
            return currentPath;
        }

        /// <summary>
        ///
        /// </summary>
        public static void ShredDirectory(string path, CipherType cipherType, bool recurse = false, bool nameObfuscation = true, bool propertyObfuscation = true)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                throw new DirectoryNotFoundException(path);
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);

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
                ObfuscateDirectoryProperties(path);

            if (nameObfuscation)
                path = ObfuscateDirectoryName(path);

            Directory.Delete(path);
        }

        /// <summary>
        ///
        /// </summary>
        public static void ShredFile(string path, CipherType cipherType, bool nameObfuscation = true, bool propertyObfuscation = true)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                throw new FileNotFoundException(path);
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            if (cipherType == CipherType.OTP)
                OTPHelper.EncryptWithoutKey(path);
            else if (cipherType == CipherType.AES)
                AESHelper.EncryptFile(AESHelper.GetNewAESKey(), path);

            if (propertyObfuscation)
                ObfuscateFileProperties(path);

            if (nameObfuscation)
                path = ObfuscateFileName(path);

            File.Delete(path);
        }

        /// <summary>
        ///
        /// </summary>
        public static string ObfuscateDirectoryName(string path, int passes = 8)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(path);
            {
                var parent = Directory.GetParent(path).FullName;
                for(var i = 0; i < passes; i++)
                {
                    var randName = FileGeneratorHelper.GetRandomNameWithoutExtension();
                    string combined;
                    while (Directory.Exists(combined = Path.Combine(parent, randName)))
                        randName = FileGeneratorHelper.GetRandomNameWithoutExtension();

                    Directory.Move(path, combined);
                    path = combined;
                }
                return path;
            }
        }

        /// <summary>
        ///
        /// </summary>
        private static byte[] CreateByteArray(long length, int number)
        {
            var value = Convert.ToByte(number);
            var array = new byte[length];
            for (var i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }
    }
}