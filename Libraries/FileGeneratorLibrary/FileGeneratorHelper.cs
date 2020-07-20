using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace FileGeneratorLibrary
{
    /// <summary>
    /// This class is used for miscellaneous file and directory tasks such as writing dummy files and obtaining unique file/directory names.
    /// </summary>
    public static class FileGeneratorHelper
    {
        private const int BUFFER_LENGTH = 1024;

        /// <summary>
        /// Creates a new file 
        /// </summary>
        /// <param name="filePath">
        /// The filepath of the file to be written.
        /// </param>
        /// <param name="length">
        /// The length of the file to be written.
        /// </param>
        public static void CreateDummyFile(string filePath, long length)
        {
            using (var fs = File.Create(filePath))
            {
                fs.SetLength(length);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void WriteNewFile(string path, long length, bool random = true, bool postDelete = true, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);

            var genFilePath = CreateUniqueFileForDirectory(path, ".BIN");

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var bw = new BinaryWriter(File.Open(genFilePath, FileMode.Open)))
                {
                    var buffer = new byte[bufferLength];

                    while (length > 0)
                    {
                        if (length < buffer.Length)
                            buffer = new byte[length];
                                                
                        if (random)
                            provider.GetNonZeroBytes(buffer);
                        bw.Write(buffer);

                        length -= buffer.Length;
                    }
                }
            }

            if (postDelete)
                File.Delete(genFilePath);
        }

        /// <summary>
        ///
        /// </summary>
        public static string CreateUniqueFileForDirectory(string directoryPath, string extension)
        {
            var uniqueFilePath = CreateUniqueFilePathForDirectory(directoryPath, extension);
            using (var fs = File.Create(uniqueFilePath))
            {
                return uniqueFilePath;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static string CreateUniqueFilePathForDirectory(string directoryPath, string extension)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath));
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException(directoryPath);

            var dir = Path.GetDirectoryName(directoryPath);
            var path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            while (File.Exists(path))
                path = Path.Combine(dir, $"{GetRandomNameWithoutExtension()}{extension}");

            return path;
        }

        /// <summary>
        ///
        /// </summary>
        public static string GetRandomNameWithoutExtension() => Path.GetRandomFileName().Split('.')[0];

        /// <summary>
        ///
        /// </summary>
        public static string GetRandomExtension() => Path.GetRandomFileName().Split('.')[1];

        /// <summary>
        /// Used to obtain a random file name (with extension).
        /// </summary>
        /// <returns>
        /// Returns a random file name with an extension. 
        /// </returns>
        public static string GetRandomNameWithExtension() => Path.GetRandomFileName();

        /// <summary>
        /// <c>GetAnonymousName()</c> should not be used to generate names for the renaming of sensitive files.
        /// The file name string that is returned by this method leaks metadata (the epoch time at which the file name was generated), use <c>GetRandomName()</c> instead.
        /// <summary>
        /// <returns>
        /// The epoch time at the time of method execution, represented as a string.
        /// </returns>
        [Obsolete("The file name string that is returned by this method leaks metadata (the epoch time at which the file name was generated), use GetRandomName() instead")]
        public static string GetAnonymousName() => Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.CurrentCulture);

        /// <summary>
        /// Writes a file to the root directory path of the DriveInfo object.
        /// </summary>
        /// <param name="driveInfo">
        /// A <see cref="DriveInfo"/> object representing the drive to be written to.
        /// </param>
        /// <param name="size">
        /// A long object representing the size of the file to be written.
        /// </param>
        /// <param name="random">
        /// A bool object, its value determining whether the file is written to with cryptographically secure bytes or not.
        /// </param>
        /// /// <param name="postDelete">
        /// A bool object, its value determining whether the file is deleted upon write completion.
        /// </param>
        /// <exception cref="ArgumentNullException">
		/// The <param name="driveInfo"/> parameter object is null.
		/// </exception>
        public static void WriteNewFile(DriveInfo driveInfo, long size, bool random = true, bool postDelete = true)
        {
            if (driveInfo == null)
                throw new ArgumentNullException(nameof(driveInfo));
            WriteNewFile(driveInfo.RootDirectory.FullName, size, random, postDelete);
        }

        public static string GetValidFileNameForDirectory(string dir, string fileName, string fileExtension)
        {
            var proposed = Path.Combine(dir, $"{fileName}{fileExtension}");
            if (!File.Exists(proposed)) return proposed;
            {
                for(var i = 0; i < 1000; i++)
                {
                    if (!File.Exists(proposed = Path.Combine(dir, $"{fileName}.{i}{fileExtension}")))
                        return proposed;
                }
            }
            return null;
        }
    }
}