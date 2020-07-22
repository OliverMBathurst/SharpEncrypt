using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class TaskManagerDisabledException : Exception
    {
        public TaskManagerDisabledException() { }

        public TaskManagerDisabledException(string message) : base(message) { }

        public TaskManagerDisabledException(string message, Exception innerException) : base(message, innerException) { }

        protected TaskManagerDisabledException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
