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
            KeySize = managed.KeySize;
            BlockSize = managed.BlockSize;
        }

        public PaddingMode Padding { get; }

        public CipherMode Mode { get; }

        public byte[] GetKey() => keyBytes;

        public byte[] GetIV() => IVBytes;

        public int KeySize { get; }

        public int BlockSize { get; }
    }
}