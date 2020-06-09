using AESLibrary;
using System;
using System.IO;
using System.Security.Cryptography;

namespace OTPLibrary
{
    public sealed class OTPInstance
    {
        private const long BUFFER_LENGTH = 1024L;

        public void GenerateKey(string path, string referenceFile, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (string.IsNullOrEmpty(referenceFile))
                throw new ArgumentNullException("genKeyOfFile");

            var fi = new FileInfo(referenceFile);
            if (!fi.Exists) throw new ArgumentException($"{referenceFile} does not exist.");

            GenerateKey(path, fi.Length, bufferLength);                
        }

        public void GenerateKey(string path, long length, long bufferLength = BUFFER_LENGTH)
        {
            if (File.Exists(path))
                throw new IOException($"{path} already exists.");

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

        public byte[] XOR(byte[] fileBytes, byte[] keyBytes)
        {
            if (fileBytes == null)
                throw new ArgumentNullException("fileBytes");
            if (keyBytes == null)
                throw new ArgumentNullException("keyBytes");
            if (fileBytes.Length > keyBytes.Length)
                throw new ArgumentException("Byte length mismatch detected.");

            for(var i = 0; i < fileBytes.Length; i++)
            {
                fileBytes[i] = (byte) (fileBytes[i] ^ keyBytes[i]);
            }

            return fileBytes;
        }

        //<summary>
        // Encrypts the file without the possibility of recovery
        //<summary>
        public void EncryptWithoutKey(string path, long bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            var readWriter = new SynchronizedReadWriter(path);
            var bytes = new byte[bufferLength];
            using (var provider = new RNGCryptoServiceProvider())
            {
                while (!readWriter.WriteComplete)
                {
                    readWriter.Read();
                    if (bytes.Length > readWriter.Buffer.Length)
                        bytes = new byte[readWriter.Buffer.Length];

                    provider.GetNonZeroBytes(bytes);
                    readWriter.SetBuffer(XOR(readWriter.Buffer, bytes));
                    readWriter.Write();
                }
            }
        }

        public void Encrypt(string filePath, string keyPath, long bufferLength = BUFFER_LENGTH) => Transform(filePath, keyPath, bufferLength);

        public void Decrypt(string filePath, string keyPath, long bufferLength = BUFFER_LENGTH) => Transform(filePath, keyPath, bufferLength);

        private void Transform(string path, string keyPath, long bufferLength)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (string.IsNullOrEmpty(keyPath))
                throw new ArgumentNullException("keyPath");
            if(!File.Exists(path))
                throw new FileNotFoundException("path");
            if (!File.Exists(keyPath))
                throw new FileNotFoundException("keyPath");
            if (new FileInfo(path).Length > new FileInfo(keyPath).Length)
                throw new ArgumentException("File length greater than key length.");

            var readWriter = new SynchronizedReadWriter(path);
            using(var keyStream = new FileStream(keyPath, FileMode.Open))
            {
                var keyBytes = new byte[bufferLength];
                while (!readWriter.WriteComplete)
                {
                    readWriter.Read(bufferLength);
                    if (readWriter.Buffer.Length < bufferLength)
                        keyBytes = new byte[readWriter.Buffer.Length];

                    keyStream.Read(keyBytes, 0, keyBytes.Length);

                    readWriter.SetBuffer(XOR(readWriter.Buffer, keyBytes));
                    readWriter.Write();
                }
            }
        }
    }
}