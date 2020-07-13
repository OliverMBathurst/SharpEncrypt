using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class OTPKeyFileNotAvailableException : Exception
    {
        public OTPKeyFileNotAvailableException() { }

        public OTPKeyFileNotAvailableException(string message) : base(message) { }

        public OTPKeyFileNotAvailableException(string message, Exception innerException) : base(message, innerException) { }

        protected OTPKeyFileNotAvailableException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
