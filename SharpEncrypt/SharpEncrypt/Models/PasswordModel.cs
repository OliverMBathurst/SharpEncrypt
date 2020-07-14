using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class PasswordModel
    {
        public string PasswordName { get; set; }

        public string Password { get; set; }

        public string Notes { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Modified { get; set; } = DateTime.Now;
    }
}