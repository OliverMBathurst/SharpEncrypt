﻿using System;

namespace OTPLibrary.Exceptions
{
    public class ByteArrayLengthMismatchException : Exception
    {
        public ByteArrayLengthMismatchException() { }

        public ByteArrayLengthMismatchException(string message) : base(message) { }

        public ByteArrayLengthMismatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}