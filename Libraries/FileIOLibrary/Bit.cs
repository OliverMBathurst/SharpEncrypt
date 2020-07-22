using System;
using System.Globalization;

namespace FileIoLibrary
{
    /// <summary>
    ///
    /// </summary>
    public struct Bit : IEquatable<Bit>
    {
        public Bit(bool bitValue) => Value = bitValue;

        /// <summary>
        ///
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int IntValue => Value ? 1 : 0;

        /// <summary>
        ///
        /// </summary>
        public void Flip() => Value = !Value;

        /// <summary>
        ///
        /// </summary>
        public override string ToString() => IntValue.ToString(CultureInfo.CurrentCulture);

        /// <summary>
        ///
        /// </summary>
        public override bool Equals(object obj) => obj is Bit b && b.Value == Value;

        /// <summary>
        ///
        /// </summary>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        ///
        /// </summary>
        public static bool operator ==(Bit left, Bit right) => left.Equals(right);

        /// <summary>
        ///
        /// </summary>
        public static bool operator !=(Bit left, Bit right) => !(left == right);

        /// <summary>
        ///
        /// </summary>
        public bool Equals(Bit other) => Value == other.Value;
    }
}