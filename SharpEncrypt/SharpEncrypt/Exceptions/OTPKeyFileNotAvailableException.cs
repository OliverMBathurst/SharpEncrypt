using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class OtpKeyFileNotAvailableException : Exception
    {
        public OtpKeyFileNotAvailableException() { }

        public OtpKeyFileNotAvailableException(string message) : base(message) { }

        public OtpKeyFileNotAvailableException(string message, Exception innerException) : base(message, innerException) { }

        protected OtpKeyFileNotAvailableException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
