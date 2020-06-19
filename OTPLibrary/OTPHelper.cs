using AESLibrary;
using OTPLibrary.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace OTPLibrary
{
    public static class OTPHelper
    {
        private const long BUFFER_LENGTH = 1024L;

        public static void GenerateKey(string path, string referenceFile, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(referenceFile))
                throw new ArgumentNullException(nameof(referenceFile));
            if (!File.Exists(referenceFile))
                throw new FileNotFoundException(referenceFile);

            GenerateKey(path, new FileInfo(referenceFile).Length, bufferLength);                
        }

        public static void GenerateKey(string path, long length, long bufferLength = BUFFER_LENGTH)
        {
            if (File.Exists(path))
                throw new FileAlreadyExistsException();

            using (var provider = new RNGCryptoServiceProvider())
            {
                using (var fs = new FileStream(path, FileMode.CreateNew))
                {
                    var byteArray = new byte[bufferLength];
                    var remainingLength = length;
                    while (remainingLength > 0)
                    {
                        if (remainingLength < bufferLength)
                            byteArray = new byte[remainingLength];

                        provider.GetNonZeroBytes(byteArray);
                        fs.Write(byteArray, 0, byteArray.Length);
                        remainingLength -= byteArray.Length;
                    }
                }
            }
        }

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
        public static void EncryptWithoutKey(string path, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var readWriter = new SynchronizedReadWriter(path);
            var bytes = new byte[bufferLength];
            using (var provider = new RNGCryptoServiceProvider())
            {
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

        public static void Encrypt(string filePath, string keyPath, long bufferLength = BUFFER_LENGTH) => Transform(filePath, keyPath, bufferLength);

        public static void Decrypt(string filePath, string keyPath, long bufferLength = BUFFER_LENGTH) => Transform(filePath, keyPath, bufferLength);

        private static void Transform(string path, string keyPath, long bufferLength)
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

            var readWriter = new SynchronizedReadWriter(path);
            using(var keyStream = new FileStream(keyPath, FileMode.Open))
            {
                var keyBytes = new byte[bufferLength];
                while (!readWriter.WriteComplete)
                {
                    readWriter.Read(bufferLength);
                    if (readWriter.GetBuffer().Length < bufferLength)
                        keyBytes = new byte[readWriter.GetBuffer().Length];

                    keyStream.Read(keyBytes, 0, keyBytes.Length);

                    readWriter.SetBuffer(XOR(readWriter.GetBuffer(), keyBytes));
                    readWriter.Write();
                }
            }
        }
    }
}