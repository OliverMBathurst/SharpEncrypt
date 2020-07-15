using System;
using System.Collections.Generic;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class PasswordModel
    {
        public string PasswordName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Modified { get; set; } = DateTime.Now;

        public DateTime LastAccess { get; set; } = DateTime.Now;

        public Stack<string> OldPasswords { get; } = new Stack<string>();
    }
}