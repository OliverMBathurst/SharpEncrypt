using System;
using System.IO;

namespace AESLibrary
{
    public sealed class SynchronizedReadWriter
    {
        private const long BUFFER_LENGTH = 1024L;
        private readonly string _path;

        public SynchronizedReadWriter(string path) => _path = path;

        public long ReadPosition { get; private set; } = 0L;

        public long WritePosition { get; private set; } = 0L;

        public bool WriteComplete { get; private set; } = false;

        public bool ReadComplete { get; private set; } = false;

        public byte[] Buffer { get; private set; } = null;

        public long DefaultBufferLength => BUFFER_LENGTH;

        public void SetBuffer(byte[] buffer)
        {
            if (Buffer == null) throw new Exception("Buffer not initialized.");
            Buffer = buffer;
        }

        public void Read(long bufferLength = BUFFER_LENGTH)
        {
            if (ReadComplete)
                return;
            if (Buffer != null)
                throw new Exception("Buffer has not been cleared by a write operation.");
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException("path");
            if (!File.Exists(_path))
                throw new Exception($"{_path} is not a valid file.");

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(ReadPosition, SeekOrigin.Begin);

                if (fs.Length - ReadPosition < bufferLength)
                    bufferLength = fs.Length - ReadPosition;

                Buffer = new byte[bufferLength];
                fs.Read(Buffer, 0, Buffer.Length);
                ReadPosition += Buffer.Length;
                SetReadingProperties(fs.Length);
            }
        }

        public void Write()
        {
            if (WriteComplete)
                return;
            if (Buffer == null)
                throw new Exception("Buffer not initialized.");
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException("path");
            if (!File.Exists(_path))
                throw new Exception($"{_path} is not a valid file.");

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(WritePosition, SeekOrigin.Begin);
                fs.Write(Buffer, 0, Buffer.Length);
                WritePosition += Buffer.Length;
                SetWritingProperties(fs.Length);
            }

            Buffer = null;            
        }

        private void SetReadingProperties(long length)
        {
            if (ReadPosition == length)
                ReadComplete = true;
        }

        private void SetWritingProperties(long length)
        {
            if (WritePosition == length || WritePosition == ReadPosition)
                WriteComplete = true;
        }
    }
}