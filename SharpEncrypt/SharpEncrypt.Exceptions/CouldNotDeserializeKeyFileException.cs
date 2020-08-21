using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class CouldNotDeserializeKeyFileException : Exception
    {
        public CouldNotDeserializeKeyFileException(string message) : base(message)
        {
        }

        public CouldNotDeserializeKeyFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CouldNotDeserializeKeyFileException()
        {
        }

        protected CouldNotDeserializeKeyFileException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
