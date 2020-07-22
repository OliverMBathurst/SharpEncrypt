using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class DuplicateIdentityException : Exception
    {
        public DuplicateIdentityException(string message) : base(message)
        {
        }

        public DuplicateIdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateIdentityException()
        {
        }

        protected DuplicateIdentityException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
