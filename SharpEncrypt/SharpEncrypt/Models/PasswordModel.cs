using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class PasswordModel
    {
        public string PasswordName { get; set; }

        public string Password { get; set; }

        public string Notes { get; set; }
    }
}