using System;

namespace SecureEraseLibrary
{
    public class DirectoryDoesNotExistOnDriveException : Exception
    {
        public DirectoryDoesNotExistOnDriveException() { }

        public DirectoryDoesNotExistOnDriveException(string message) : base(message) { }

        public DirectoryDoesNotExistOnDriveException(string message, Exception innerException) : base(message, innerException) { }
    }
}
