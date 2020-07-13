using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class OTPPasswordStoreFirstUseException : Exception
    {
        public OTPPasswordStoreFirstUseException(string message) : base(message)
        {
        }

        public OTPPasswordStoreFirstUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OTPPasswordStoreFirstUseException()
        {
        }

        protected OTPPasswordStoreFirstUseException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
