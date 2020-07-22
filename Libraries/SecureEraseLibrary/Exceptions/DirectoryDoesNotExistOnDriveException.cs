using System;

namespace SecureEraseLibrary.Exceptions
{
    public class DirectoryDoesNotExistOnDriveException : Exception
    {
        public DirectoryDoesNotExistOnDriveException() { }

        public DirectoryDoesNotExistOnDriveException(string message) : base(message) { }

        public DirectoryDoesNotExistOnDriveException(string message, Exception innerException) : base(message, innerException) { }
    }
}
