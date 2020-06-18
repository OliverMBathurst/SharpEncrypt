using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public sealed class InvalidKeyException : Exception
    {
        public InvalidKeyException() { }

        public InvalidKeyException(string message) : base(message) { }      

        public InvalidKeyException(string message, Exception innerException) : base(message, innerException) { }

        private InvalidKeyException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            => throw new NotImplementedException();
    }
}
