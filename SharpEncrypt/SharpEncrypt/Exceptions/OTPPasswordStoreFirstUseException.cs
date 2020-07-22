using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class OtpPasswordStoreFirstUseException : Exception
    {
        public OtpPasswordStoreFirstUseException(string message) : base(message)
        {
        }

        public OtpPasswordStoreFirstUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OtpPasswordStoreFirstUseException()
        {
        }

        protected OtpPasswordStoreFirstUseException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
