using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class InvalidEncryptedFileException : Exception
    {
        public InvalidEncryptedFileException(string message) : base(message)
        {
        }

        public InvalidEncryptedFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidEncryptedFileException()
        {
        }

        protected InvalidEncryptedFileException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
