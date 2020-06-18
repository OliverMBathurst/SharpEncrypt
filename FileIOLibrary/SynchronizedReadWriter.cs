using System;
using System.IO;

namespace AESLibrary
{
    public sealed class SynchronizedReadWriter
    {
        private const long BUFFER_LENGTH = 1024L;
        private readonly string _path;
        private byte[] buffer;

        public SynchronizedReadWriter(string path) => _path = path;

        public long ReadPosition { get; private set; } = 0L;

        public long WritePosition { get; private set; } = 0L;

        public bool WriteComplete { get; private set; } = false;

        public bool ReadComplete { get; private set; } = false;

        public byte[] GetBuffer() => buffer;

        public static long DefaultBufferLength => BUFFER_LENGTH;

        public void SetBuffer(byte[] bufferParam)
        {
            if (buffer == null) throw new Exception("Buffer not initialized.");
            buffer = bufferParam;
        }

        public void Read(long bufferLength = BUFFER_LENGTH)
        {
            if (ReadComplete)
                return;
            if (buffer != null)
                throw new Exception("Buffer has not been cleared by a write operation.");
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException(nameof(_path));
            if (!File.Exists(_path))
                throw new Exception($"{_path} is not a valid file.");

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(ReadPosition, SeekOrigin.Begin);

                if (fs.Length - ReadPosition < bufferLength)
                    bufferLength = fs.Length - ReadPosition;

                buffer = new byte[bufferLength];
                fs.Read(buffer, 0, buffer.Length);
                ReadPosition += buffer.Length;
                SetReadingProperties(fs.Length);
            }
        }

        public void Write()
        {
            if (WriteComplete)
                return;
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException(nameof(_path));
            if (!File.Exists(_path))
                throw new Exception($"{_path} is not a valid file.");

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(WritePosition, SeekOrigin.Begin);
                fs.Write(buffer, 0, buffer.Length);
                WritePosition += buffer.Length;
                SetWritingProperties(fs.Length);
            }

            buffer = null;            
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