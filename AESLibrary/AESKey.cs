using System;
using System.Security.Cryptography;

namespace AESLibrary
{
    [Serializable]
    public sealed class AESKey
    {
        public AESKey(RijndaelManaged managed)
        {
            Key = managed.Key;
            IV = managed.IV;
            KeySize = managed.KeySize;
            BlockSize = managed.BlockSize;
        }

        public byte[] Key { get; }

        public byte[] IV { get; }

        public int KeySize { get; }

        public int BlockSize { get; }
    }
}
