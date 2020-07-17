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
            Padding = managed.Padding;
            Mode = managed.Mode;
        }

        public byte[] GetKey() => keyBytes;

        public byte[] GetIV() => IVBytes;

        public int KeySize { get; } = 256;

        public int BlockSize { get; } = 128;

        public PaddingMode Padding { get; } = PaddingMode.PKCS7;

        public CipherMode Mode { get; } = CipherMode.CBC;
    }
}