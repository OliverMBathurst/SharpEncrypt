using FileIOLibrary;
using OTPLibrary.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace OTPLibrary
{
    /// <summary>
    ///
    /// </summary>
    public static class OTPHelper
    {
        private const int BUFFER_LENGTH = 1024;

        /// <summary>
        ///
        /// </summary>
        public static void GenerateKey(string path, string referenceFile, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(referenceFile))
                throw new ArgumentNullException(nameof(referenceFile));
            if (!File.Exists(referenceFile))
                throw new FileNotFoundException(referenceFile);

            GenerateKey(path, new FileInfo(referenceFile).Length, bufferLength);                
        }

        /// <summary>
        ///
        /// </summary>
        public static void GenerateKey(string path, long length, int bufferLength = BUFFER_LENGTH)
        {
            if (File.Exists(path))
                throw new FileAlreadyExistsException();

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.CreateNew))
                {
                    var byteArray = new byte[bufferLength];
                    while (length > 0)
                    {
                        if (length < bufferLength)
                            byteArray = new byte[length];

                        provider.GetNonZeroBytes(byteArray);
                        fs.Write(byteArray, 0, byteArray.Length);
                        length -= byteArray.Length;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static byte[] XOR(byte[] fileBytes, byte[] keyBytes)
        {
            if (fileBytes == null)
                throw new ArgumentNullException(nameof(fileBytes));
            if (keyBytes == null)
                throw new ArgumentNullException(nameof(keyBytes));
            if (fileBytes.Length > keyBytes.Length)
                throw new ByteArrayLengthMismatchException();

            for(var i = 0; i < fileBytes.Length; i++)
            {
                fileBytes[i] = (byte) (fileBytes[i] ^ keyBytes[i]);
            }

            return fileBytes;
        }

        //<summary>
        // Encrypts the file without the possibility of recovery
        //<summary>
        public static void EncryptWithoutKey(string path, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
                        
            using (var provider = new RNGCryptoServiceProvider())
            {
                var readWriter = new SynchronizedReadWriter(path);
                var bytes = new byte[bufferLength];
                while (!readWriter.WriteComplete)
                {
                    readWriter.Read();
                    if (bytes.Length > readWriter.GetBuffer().Length)
                        bytes = new byte[readWriter.GetBuffer().Length];

                    provider.GetNonZeroBytes(bytes);
                    readWriter.SetBuffer(XOR(readWriter.GetBuffer(), bytes));
                    readWriter.Write();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Transform(string path, string keyPath, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(keyPath))
                throw new ArgumentNullException(nameof(keyPath));

            if (!File.Exists(path))
                throw new FileNotFoundException(path);
            if (!File.Exists(keyPath))
                throw new FileNotFoundException(keyPath);

            if (new FileInfo(path).Length > new FileInfo(keyPath).Length)
                throw new FileLengthGreaterThanKeyLengthException();
                        
            using(var keyStream = new FileStream(keyPath, FileMode.Open))
            {
                var readWriter = new SynchronizedReadWriter(path);
                var keyBytes = new byte[bufferLength];
                while (!readWriter.WriteComplete)
                {
                    readWriter.Read(keyBytes.Length);
                    if (readWriter.GetBuffer().Length < keyBytes.Length)
                        keyBytes = new byte[readWriter.GetBuffer().Length];

                    keyStream.Read(keyBytes, 0, keyBytes.Length);

                    readWriter.SetBuffer(XOR(readWriter.GetBuffer(), keyBytes));
                    readWriter.Write();
                }
            }
        }
    }
}