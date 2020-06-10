using System;

namespace FileIOLibrary
{
    public class Byte
    {        
        private const char on = '1', off = '0';
        private string byteString = "00000000";

        public Byte(string stringRepresentation) => ByteString = stringRepresentation;

        public Bit[] ToArray() => GetBits();

        public string ByteString 
        {
            get => byteString;
            set 
            {
                if (value.Length != 8)
                    throw new ArgumentException("String not of length 8");
                for (var i = 0; i < value.Length; i++)
                    if (value[i] != on && value[i] != off)
                        throw new ArgumentException($"Invalid char at index {i}.");
                byteString = value;
            }
        }

        public override string ToString() => ByteString;

        public int ToInt(EndianMode mode = EndianMode.Big)
        {
            var sum = 0;
            for(var i = 0; i < 8; i++)
                sum += ByteString[i] == on ? (int)Math.Pow(2, mode == EndianMode.Big ? (8 - (i + 1)) : i) : 0;
            return sum;
        }

        public void Not()
        {
            var str = string.Empty;
            for (var i = 0; i < 8; i++)
                str += ByteString[i] == on ? off : on;
            ByteString = str;
        }

        private Bit[] GetBits() 
        {
            var bitsArray = new Bit[8];          
            for(var i = 0; i < 8; i++)
                bitsArray[i] = new Bit(ByteString[i] == on);
            return bitsArray;
        }
    }
}