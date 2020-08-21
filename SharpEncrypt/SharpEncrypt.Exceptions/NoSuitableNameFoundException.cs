using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class NoSuitableNameFoundException : Exception
    {
        public NoSuitableNameFoundException() { }

        public NoSuitableNameFoundException(string message) : base(message) { }

        public NoSuitableNameFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected NoSuitableNameFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
