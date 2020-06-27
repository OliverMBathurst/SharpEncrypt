using System;
using System.Globalization;
using System.IO;
using System.Resources;

namespace FileIOLibrary
{
    /// <summary>
    ///
    /// </summary>
    public sealed class SynchronizedReadWriter
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));
        private readonly string Path;
        private const long BUFFER_LENGTH = 1024;
        private byte[] buffer;

        public SynchronizedReadWriter(string path) => Path = path;

        /// <summary>
        ///
        /// </summary>
        public long ReadPosition { get; private set; } = 0L;

        /// <summary>
        ///
        /// </summary>
        public long WritePosition { get; private set; } = 0L;

        /// <summary>
        ///
        /// </summary>
        public bool WriteComplete { get; private set; } = false;

        /// <summary>
        ///
        /// </summary>
        public bool ReadComplete { get; private set; } = false;

        /// <summary>
        ///
        /// </summary>
        public byte[] GetBuffer() => buffer;

        /// <summary>
        ///
        /// </summary>
        public static long DefaultBufferLength => BUFFER_LENGTH;

        /// <summary>
        ///
        /// </summary>
        public void SetBuffer(byte[] bufferParam)
        {
            if (buffer == null) throw new ArgumentNullException(ResourceManager.GetString("BufferNotInitialized", CultureInfo.CurrentCulture));
            buffer = bufferParam;
        }

        /// <summary>
        ///
        /// </summary>
        public void Read(long bufferLength = BUFFER_LENGTH)
        {
            if (ReadComplete)
                return;
            if (buffer != null)
                throw new Exception(ResourceManager.GetString("BufferNotCleared", CultureInfo.CurrentCulture));
            if (string.IsNullOrEmpty(Path))
                throw new ArgumentNullException(ResourceManager.GetString("path", CultureInfo.CurrentCulture));
            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);

            using (var fs = new FileStream(Path, FileMode.Open))
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

        /// <summary>
        ///
        /// </summary>
        public void Write()
        {
            if (WriteComplete)
                return;
            if (buffer == null)
                throw new ArgumentNullException(ResourceManager.GetString("buffer", CultureInfo.CurrentCulture));
            if (string.IsNullOrEmpty(Path))
                throw new ArgumentNullException(ResourceManager.GetString("path", CultureInfo.CurrentCulture));
            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);

            using (var fs = new FileStream(Path, FileMode.Open))
            {
                fs.Seek(WritePosition, SeekOrigin.Begin);
                fs.Write(buffer, 0, buffer.Length);
                WritePosition += buffer.Length;
                SetWritingProperties(fs.Length);
            }

            buffer = null;            
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