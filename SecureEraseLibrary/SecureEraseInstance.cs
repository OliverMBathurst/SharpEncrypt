using System;
using System.IO;
using System.Security.Cryptography;

namespace SecureEraseLibrary
{
    public sealed class SecureEraseInstance
    {
        public void WriteRandomData(string path, int passes = 1)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a file");

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    for (var i = 0; i < passes; i++)
                    {
                        fs.Seek(0, SeekOrigin.Begin);
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
        }

        public void ObfuscateFileProperties(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a file");

            var unixTime = new DateTime(1970, 1, 1);
            File.SetCreationTime(path, unixTime);
            File.SetCreationTimeUtc(path, unixTime);
            File.SetLastAccessTime(path, unixTime);
            File.SetLastAccessTimeUtc(path, unixTime);
            File.SetLastWriteTime(path, unixTime);
            File.SetLastWriteTimeUtc(path, unixTime);
        }

        public void ObfuscateDirectoryProperties(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a directory");

            var unixTime = new DateTime(1970, 1, 1);
            Directory.SetCreationTime(path, unixTime);
            Directory.SetCreationTimeUtc(path, unixTime);
            Directory.SetLastAccessTime(path, unixTime);
            Directory.SetLastAccessTimeUtc(path, unixTime);
            Directory.SetLastWriteTime(path, unixTime);
            Directory.SetLastWriteTimeUtc(path, unixTime);
        }

        public string ObfuscateFileName(string path, int passes = 8)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a file");

            var currentPath = path;
            var dir = Path.GetDirectoryName(path);
            for (var i = 0; i < passes; i++)
            {
                var rand = GetRandomName();
                while (File.Exists(Path.Combine(dir, rand)))
                    rand = GetRandomName();

                File.Move(currentPath, rand);
                currentPath = Path.Combine(dir, rand);
            }
            return currentPath;
        }

        public string CreateUniqueFileForDirectory(string directoryPath, string extension)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException("dirPath");
            if (!Directory.Exists(directoryPath))
                throw new IOException($"{directoryPath} is not a valid directory");

            var dir = Path.GetDirectoryName(directoryPath);
            var path = Path.Combine(dir, $"{GetRandomName()}{extension}");

            while(File.Exists(path))
                path = Path.Combine(dir, $"{GetRandomName()}{extension}");

            File.Create(path);
            return path;
        }

        public void ShredDirectory(string path, CipherType cipherType, bool recurse, bool nameObfuscation, bool propertyObfuscation)
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
                ObfuscateDirectoryProperties(path);

            if (nameObfuscation)
            {
                var result = ObfuscateDirectoryName(path);
                if (result != null)
                    path = result;
            }

            Directory.Delete(path);
        }

        public void ShredFile(string path, CipherType cipherType, bool nameObfuscation, bool propertyObfuscation)
        {
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                throw new ArgumentException($"{path} is not a file.");
            if (!new FileInfo(path).Exists)
                throw new ArgumentException($"{path} does not exist.");

            //TODO: Fix all this one individual projects have been completed, keys also should only be used once
            if (cipherType == CipherType.OTP)
            {

            }
            else
            {

            }

            if (propertyObfuscation)
                ObfuscateFileProperties(path);

            if (nameObfuscation)
            {
                var result = ObfuscateFileName(path);
                if (result != null)
                    path = result;
            }

            File.Delete(path);
        }

        public string ObfuscateDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (Directory.Exists(path))
            {
                var parent = Directory.GetParent(path).FullName;
                var randName = GetRandomName();
                while (Directory.Exists(Path.Combine(parent, randName)))
                    randName = GetRandomName();

                Directory.Move(path, randName);
                return Path.Combine(parent, randName);
            }
            else
            {
                throw new ArgumentException($"{path} is not a valid path of a directory.");
            }
        }


        public string GetRandomName()
            => Path.GetRandomFileName().Split('.')[0];

        ///<summary>
        ///<c>GetAnonymousName()</c> should not be used to generate names for the renaming of sensitive files.
        ///The file name string that is returned by this method leaks metadata (the epoch time at which the file name was generated), use <c>GetRandomName()</c> instead.
        ///<summary>
        public string GetAnonymousName()
            => Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString();
    }
}