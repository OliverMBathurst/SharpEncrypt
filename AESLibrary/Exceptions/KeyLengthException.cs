﻿using System;

namespace AESLibrary
{
    public class KeyLengthException : Exception
    {
        public KeyLengthException() { }

        public KeyLengthException(string message) : base(message) { }

        public KeyLengthException(string message, Exception innerException) : base(message, innerException) { }
    }
}
