using System;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace FileIoLibrary
{
    /// <summary>
    ///
    /// </summary>
    public class FileIoByte : IEquatable<FileIoByte>
    {
        private readonly ResourceManager _resourceManager = new ResourceManager(typeof(Resources));
        private const char On = '1', Off = '0';
        private Bit[] _bits;

        /// <summary>
        ///
        /// </summary>
        public FileIoByte(string stringRepresentation) => SetBits(stringRepresentation);

        /// <summary>
        ///
        /// </summary>
        public FileIoByte(Bit[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException(nameof(bits));
            if (bits.Length != 8)
                throw new ArgumentException(_resourceManager.GetString("WrongStringLength", CultureInfo.CurrentCulture));
            _bits = bits;
        }

        /// <summary>
        ///
        /// </summary>
        public Bit[] GetBits() => _bits;

        /// <summary>
        ///
        /// </summary>
        public int ToInt(EndianMode mode = EndianMode.Big)
        {
            var sum = 0;
            for (var i = 0; i < 8; i++)
                sum += _bits[i].Value ? (int)Math.Pow(2, mode == EndianMode.Big ? (8 - (i + 1)) : i) : 0;
            return sum;
        }

        /// <summary>
        ///
        /// </summary>
        public void Not()
        {
            for (var i = 0; i < 8; i++)
            {
                _bits[i].Value = !_bits[i].Value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Equals(FileIoByte other)
        {
            return other != null && other.GetBits().SequenceEqual(GetBits());
        }

        /// <summary>
        /// Returns the Bit object at the specified index of the FileIOByte's Bit array.
        /// </summary>
        /// <param name="index">
        /// The index of the Bit object to return.
        /// </param>
        /// <returns>
        /// The Bit object at the specified index.
        /// </returns>
        public Bit this[int index] => _bits[index];

        /// <summary>
        ///
        /// </summary>
        public static FileIoByte DefaultByte
            => new FileIoByte(new [] { new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit()});

        /// <summary>
        ///
        /// </summary>
        public static FileIoByte New => DefaultByte;

        /// <summary>
        ///
        /// </summary>
        public override bool Equals(object obj) => obj is FileIoByte b && Equals(b);

        /// <summary>
        ///
        /// </summary>
        public override string ToString() => string.Join(string.Empty, _bits);

        /// <summary>
        ///
        /// </summary>
        private void SetBits(string byteString)
        {
            if (byteString == null)
                throw new ArgumentNullException(nameof(byteString));
            if (byteString.Length != 8)
                throw new ArgumentException(Resources.WrongStringLength);
            
            var bits = new Bit[8];
            for (var i = 0; i < byteString.Length; i++)
            {
                switch (byteString[i])
                {
                    case On:
                        bits[i] = new Bit(true);
                        break;
                    case Off:
                        bits[i] = new Bit(false);
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidCharAt, i));            
                }
            }

            _bits = bits;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}