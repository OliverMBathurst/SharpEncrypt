using System;
using System.Security.Cryptography;

namespace AESLibrary
{
    [Serializable]
    public sealed class AESKey
    {
        private readonly byte[] keyBytes;
        private readonly byte[] IVBytes;

        public AESKey(RijndaelManaged managed)
        {
            if (managed == null)
                throw new ArgumentNullException(nameof(managed));

            keyBytes = managed.Key;
            IVBytes = managed.IV;
            KeyLength = managed.KeySize;
        }

        public byte[] GetKey() => keyBytes;

        public byte[] GetIV() => IVBytes;

        public int KeyLength { get; }
    }
}
