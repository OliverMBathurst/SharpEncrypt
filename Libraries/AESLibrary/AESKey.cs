using System;
using System.Security.Cryptography;

namespace AESLibrary
{
    [Serializable]
    public sealed class AesKey
    {
        private readonly byte[] _keyBytes;
        private readonly byte[] _ivBytes;

        public AesKey(RijndaelManaged managed)
        {
            if (managed == null)
                throw new ArgumentNullException(nameof(managed));

            _keyBytes = managed.Key;
            _ivBytes = managed.IV;
            KeySize = managed.KeySize;
            BlockSize = managed.BlockSize;
            Padding = managed.Padding;
            Mode = managed.Mode;
        }

        public byte[] GetKey() => _keyBytes;

        public byte[] GetIv() => _ivBytes;

        public int KeySize { get; }

        public int BlockSize { get; }

        public PaddingMode Padding { get; }

        public CipherMode Mode { get; }
    }
}