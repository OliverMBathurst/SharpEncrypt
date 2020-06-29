using System;

namespace OTPLibrary.Exceptions
{
    public class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException() { }

        public FileAlreadyExistsException(string message) : base(message) { }

        public FileAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
