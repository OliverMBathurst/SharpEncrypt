using System;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace FileIOLibrary
{
    /// <summary>
    ///
    /// </summary>
    public class FileIOByte : IEquatable<FileIOByte>
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));
        private const char ON = '1', OFF = '0';
        private Bit[] mBits;

        /// <summary>
        ///
        /// </summary>
        public FileIOByte(string stringRepresentation) => SetBits(stringRepresentation);

        /// <summary>
        ///
        /// </summary>
        public FileIOByte(Bit[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException(nameof(bits));
            if (bits.Length != 8)
                throw new ArgumentException(ResourceManager.GetString("WrongStringLength", CultureInfo.CurrentCulture));
            mBits = bits;
        }

        /// <summary>
        ///
        /// </summary>
        public Bit[] GetBits() => mBits;

        /// <summary>
        ///
        /// </summary>
        public int ToInt(EndianMode mode = EndianMode.Big)
        {
            var sum = 0;
            for (var i = 0; i < 8; i++)
                sum += mBits[i].Value == true ? (int)Math.Pow(2, mode == EndianMode.Big ? (8 - (i + 1)) : i) : 0;
            return sum;
        }

        /// <summary>
        ///
        /// </summary>
        public void Not()
        {
            for (var i = 0; i < 8; i++)
            {
                if (mBits[i].Value)
                {
                    mBits[i].Value = false;
                }
                else
                {
                    mBits[i].Value = true;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Equals(FileIOByte other)
        {
            if (other != null)
                return other.GetBits().SequenceEqual(GetBits());
            return false;
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
        public Bit this[int index] => mBits[index];

        /// <summary>
        ///
        /// </summary>
        public static FileIOByte DefaultByte
            => new FileIOByte(new [] { new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit(), new Bit()});

        /// <summary>
        ///
        /// </summary>
        public static FileIOByte New => DefaultByte;

        /// <summary>
        ///
        /// </summary>
        public override bool Equals(object obj) => obj is FileIOByte b && Equals(b);

        /// <summary>
        ///
        /// </summary>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        ///
        /// </summary>
        public override string ToString() => string.Join(string.Empty, mBits);

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
                    case ON:
                        bits[i] = new Bit(true);
                        break;
                    case OFF:
                        bits[i] = new Bit(false);
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidCharAt, i));            
                }
            }

            mBits = bits;
        }
    }
}