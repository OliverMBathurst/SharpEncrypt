using SharpEncrypt.Enums;
using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class DriveWipeJob
    {
        public DriveWipeType WipeType { get; set; }

        public int WipeRounds { get; set; }

        public int NameObfuscationRounds { get; set; }

        public int PropertyObfuscationRounds { get; set; }
    }
}
