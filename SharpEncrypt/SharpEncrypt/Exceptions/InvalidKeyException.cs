using System;

namespace SharpEncrypt.Exceptions
{
    internal sealed class InvalidKeyException : Exception
    {
        public InvalidKeyException(string message) : base(message) { }
    }
}
