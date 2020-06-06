using FileGeneratorLibrary;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SecureEraseLibrary
{
    public sealed class SecureEraseInstance
    {
        private readonly FileGeneratorInstance _fileGeneratorInstance = new FileGeneratorInstance();

        public void WriteZeros(string path, int passes = 1)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a file");

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var byteArr = CreateByteArray(1024, 0);
                for (var i = 0; i < passes; i++)
                {
                    fs.Seek(0, SeekOrigin.Begin);                    
                    var remainingLength = fs.Length;
                    while (remainingLength > 0)
                    {
                        if (remainingLength < 1024)
                            byteArr = CreateByteArray(remainingLength, 0);

                        fs.Write(byteArr, 0, byteArr.Length);
                        remainingLength -= byteArr.Length;
                    }
                }
            }
        }

        public void Write255s(string path, int passes = 1)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new IOException($"{path} is not a valid path of a file");

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var byteArr = CreateByteArray(1024, 255);
                    for (var i = 0; i < passes; i++)
                    {
                        fs.Seek(0, SeekOrigin.Begin);                        
                        var remainingLength = fs.Length;
                        while (remainingLength > 0)
                        {
                            if (remainingLength < 1024)
                                byteArr = CreateByteArray(remainingLength, 255);


                            fs.Write(byteArr, 0, byteArr.Length);
                            remainingLength -= byteArr.Length;
                        }
                    }
                }
            }
        }

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
                        var remainingLength = fs.Length;
                        while (remainingLength > 0)
                        {
                            if (remainingLength < 1024)
                                byteArr = new byte[remainingLength];

                            provider.GetNonZeroBytes(byteArr);
                            fs.Write(byteArr, 0, byteArr.Length);
                            remainingLength -= byteArr.Length;
                        }
                    }
                }
            }
        }

        public void SDeleteDriveWipe(char driveLetter, string tempDirectory = "", bool postDelete = true, int passes = 1)
        {
            var drives = DriveInfo.GetDrives().Where(x => char.ToLower(x.Name[0]) == char.ToLower(driveLetter));
            if (!drives.Any())
                throw new ArgumentException($"{driveLetter} is not a valid drive letter.");

            var drive = drives.First();
            if(!string.IsNullOrEmpty(tempDirectory) && Directory.Exists(tempDirectory))
            {
                if(char.ToLower(Path.GetPathRoot(tempDirectory)[0]) == char.ToLower(driveLetter))
                {
                    var tmpFile = _fileGeneratorInstance.CreateUniqueFilePathForDirectory(tempDirectory, _fileGeneratorInstance.GetRandomExtension());
                    _fileGeneratorInstance.CreateDummyFile(tmpFile, drive.AvailableFreeSpace);
                    WriteZeros(tmpFile, passes);
                    Write255s(tmpFile, passes);
                    WriteRandomData(tmpFile, passes);
                    if(postDelete)
                        File.Delete(tmpFile);
                }
                else
                {
                    throw new ArgumentException($"The provided directory {tempDirectory} does not exist on drive {driveLetter}");
                }
            }
        }

        public void SDeleteFileWipe(string filePath, bool postDelete = true)
        {
            WriteZeros(filePath);
            Write255s(filePath);
            WriteRandomData(filePath);
            if (postDelete)
                File.Delete(filePath);
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
                var rand = _fileGeneratorInstance.GetRandomNameWithoutExtension();
                while (File.Exists(Path.Combine(dir, rand)))
                    rand = _fileGeneratorInstance.GetRandomNameWithoutExtension();

                File.Move(currentPath, rand);
                currentPath = Path.Combine(dir, rand);
            }
            return currentPath;
        }

        public void ShredDirectory(string path, CipherType cipherType, bool recurse = false, bool nameObfuscation = true, bool propertyObfuscation = true)
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
                path = ObfuscateDirectoryName(path);

            Directory.Delete(path);
        }

        public void ShredFile(string path, CipherType cipherType, bool nameObfuscation = true, bool propertyObfuscation = true)
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
                path = ObfuscateFileName(path);

            File.Delete(path);
        }

        public string ObfuscateDirectoryName(string path, int passes = 8)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (Directory.Exists(path))
            {
                var parent = Directory.GetParent(path).FullName;
                var obfuscatedPath = path;
                for(var i = 0; i < passes; i++)
                {                    
                    var randName = _fileGeneratorInstance.GetRandomNameWithoutExtension();
                    while (Directory.Exists(Path.Combine(parent, randName)))
                        randName = _fileGeneratorInstance.GetRandomNameWithoutExtension();

                    Directory.Move(obfuscatedPath, randName);
                    obfuscatedPath = Path.Combine(parent, randName);
                }
                return obfuscatedPath;
            }
            else
            {
                throw new ArgumentException($"{path} is not a valid path of a directory.");
            }
        }

        private byte[] CreateByteArray(long length, int number)
        {
            var value = Convert.ToByte(number);
            var array = new byte[length];
            for (var i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }
    }
}