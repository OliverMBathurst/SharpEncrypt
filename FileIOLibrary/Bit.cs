using System;

namespace FileIOLibrary
{
    public struct Bit : IEquatable<Bit>
    {
        public Bit(bool bitValue) => Value = bitValue;

        public bool Value { get; set; }

        public int IntValue => Value ? 1 : 0;

        public void Flip() => Value = !Value;

        public override bool Equals(object obj) => obj is Bit b && b.Value == Value;

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Bit left, Bit right) => left.Equals(right);

        public static bool operator !=(Bit left, Bit right) => !(left == right);

        public bool Equals(Bit other) => Value == other.Value;
    }
}
