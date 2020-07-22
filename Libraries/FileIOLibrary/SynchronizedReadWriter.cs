using System;
using System.Globalization;
using System.IO;
using System.Resources;

namespace FileIoLibrary
{
    /// <summary>
    ///
    /// </summary>
    public sealed class SynchronizedReadWriter
    {
        private readonly ResourceManager _resourceManager = new ResourceManager(typeof(Resources));
        private readonly string _path;
        private const long BufferLength = 1024;
        private byte[] _buffer;

        public SynchronizedReadWriter(string path) => _path = path;

        /// <summary>
        ///
        /// </summary>
        public long ReadPosition { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public long WritePosition { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool WriteComplete { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool ReadComplete { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public byte[] GetBuffer() => _buffer;

        /// <summary>
        ///
        /// </summary>
        public static long DefaultBufferLength => BufferLength;

        /// <summary>
        ///
        /// </summary>
        public void SetBuffer(byte[] bufferParam)
        {
            if (_buffer == null) throw new ArgumentNullException(_resourceManager.GetString("BufferNotInitialized", CultureInfo.CurrentCulture));
            _buffer = bufferParam;
        }

        /// <summary>
        ///
        /// </summary>
        public void Read(long bufferLength = BufferLength)
        {
            if (ReadComplete)
                return;
            if (_buffer != null)
                throw new Exception(_resourceManager.GetString("BufferNotCleared", CultureInfo.CurrentCulture));
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException(_resourceManager.GetString("path", CultureInfo.CurrentCulture));
            if (!File.Exists(_path))
                throw new FileNotFoundException(_path);

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(ReadPosition, SeekOrigin.Begin);

                if (fs.Length - ReadPosition < bufferLength)
                    bufferLength = fs.Length - ReadPosition;

                _buffer = new byte[bufferLength];
                fs.Read(_buffer, 0, _buffer.Length);
                ReadPosition += _buffer.Length;
                SetReadingProperties(fs.Length);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Write()
        {
            if (WriteComplete)
                return;
            if (_buffer == null)
                throw new ArgumentNullException(_resourceManager.GetString("buffer", CultureInfo.CurrentCulture));
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException(_resourceManager.GetString("path", CultureInfo.CurrentCulture));
            if (!File.Exists(_path))
                throw new FileNotFoundException(_path);

            using (var fs = new FileStream(_path, FileMode.Open))
            {
                fs.Seek(WritePosition, SeekOrigin.Begin);
                fs.Write(_buffer, 0, _buffer.Length);
                WritePosition += _buffer.Length;
                SetWritingProperties(fs.Length);
            }

            _buffer = null;            
        }

        /// <summary>
        ///
        /// </summary>
        private void SetReadingProperties(long length)
        {
            if (ReadPosition == length)
                ReadComplete = true;
        }

        /// <summary>
        ///
        /// </summary>
        private void SetWritingProperties(long length)
        {
            if (WritePosition == length || WritePosition == ReadPosition)
                WriteComplete = true;
        }
    }
}