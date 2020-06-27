using System;
using System.IO;
using System.Security.Cryptography;

namespace GutmannLibrary
{
    public static class GutmannHelper
    {
        private readonly static int[] _patterns = new[] { 
            85, 170, 146, 73, 
            36, 0, 17, 34, 
            51, 68, 85, 102, 
            119, 136, 153, 170,
            187, 204, 221, 238,
            255, 146, 73, 36,
            109, 182, 219
        };

        private const int BUFFER_LENGTH = 1024;

        /// <summary>
        ///
        /// </summary>
        public static void WipeFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            RandomWipe(path);
            GutmannPatternWipe(path);
            RandomWipe(path);
        }

        /// <summary>
        ///
        /// </summary>
        public static void RandomWipe(string path, int passes = 4, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var bw = new BinaryWriter(File.OpenWrite(path)))
            {
                using (var provider = new RNGCryptoServiceProvider())
                {
                    for (var i = 0; i < passes; i++)
                    {
                        bw.Seek(0, SeekOrigin.Begin);
                        var remainingLength = bw.BaseStream.Length;
                        var bytes = new byte[bufferLength];
                        while (remainingLength > 0)
                        {
                            if (remainingLength < bytes.Length)
                                bytes = new byte[remainingLength];

                            provider.GetNonZeroBytes(bytes);
                            bw.Write(bytes);
                            remainingLength -= bytes.Length;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void GutmannPatternWipe(string path, int bufferLength = BUFFER_LENGTH)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using (var bw = new BinaryWriter(File.OpenWrite(path)))
            {
                foreach(var number in _patterns)
                {
                    bw.Seek(0, SeekOrigin.Begin);
                    var remainingLength = bw.BaseStream.Length;
                    var bytes = new byte[bufferLength];
                    while (remainingLength > 0)
                    {
                        if (remainingLength < bytes.Length)
                            bytes = new byte[remainingLength];

                        bw.Write(Fill(bytes, number));
                        remainingLength -= bytes.Length;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private static byte[] Fill(byte[] array, int number)
        {
            var value = (byte)number;
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }
    }
}