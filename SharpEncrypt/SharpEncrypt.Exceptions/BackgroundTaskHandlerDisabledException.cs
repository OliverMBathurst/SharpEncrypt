using System;

namespace SharpEncrypt.Exceptions
{
    [Serializable]
    public class BackgroundTaskHandlerDisabledException : Exception
    {
        public BackgroundTaskHandlerDisabledException() { }

        public BackgroundTaskHandlerDisabledException(string message) : base(message) { }

        public BackgroundTaskHandlerDisabledException(string message, Exception innerException) : base(message, innerException) { }

        protected BackgroundTaskHandlerDisabledException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
