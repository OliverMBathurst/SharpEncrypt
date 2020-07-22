using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class FileNotSecuredException : Exception
    {
        public FileNotSecuredException(string message) : base(message)
        {
        }

        public FileNotSecuredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FileNotSecuredException()
        {
        }

        protected FileNotSecuredException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
