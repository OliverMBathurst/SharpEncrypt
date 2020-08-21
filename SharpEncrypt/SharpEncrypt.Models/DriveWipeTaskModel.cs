using System;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class DriveWipeTaskModel
    {
        public DriveWipeType WipeType { get; set; }

        public int WipeRounds { get; set; }

        public int NameObfuscationRounds { get; set; }

        public int PropertyObfuscationRounds { get; set; }
    }
}
