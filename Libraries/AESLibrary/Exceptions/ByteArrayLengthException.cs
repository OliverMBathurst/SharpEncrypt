using System;

namespace AESLibrary
{
    public class ByteArrayLengthException : Exception
    {
        public ByteArrayLengthException() { }

        public ByteArrayLengthException(string message) : base(message) { }

        public ByteArrayLengthException(string message, Exception innerException) : base(message, innerException) { }
    }
}
