namespace FileIOLibrary
{
    public struct Bit
    {
        public Bit(bool bitValue) => Value = bitValue;

        public bool Value { get; set; }

        public int IntValue => Value ? 1 : 0;

        public void Flip() => Value = !Value;
    }
}
