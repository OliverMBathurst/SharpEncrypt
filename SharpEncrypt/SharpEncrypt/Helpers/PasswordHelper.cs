using System;
using System.Linq;

namespace SharpEncrypt.Helpers
{
    internal sealed class PasswordHelper
    {
        private readonly char[] SpecialChars = { '<', '>', '?', '!', '£', '$', '%', '^', '&', '*', '(', ')' };
        private readonly char[] Alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly char[] Numerics = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly char[] RestrictedChars = { '<', '>', '\'', '\\' };

        public string GeneratePassword()
        {
            var passwordString = string.Empty;
            var rng = new Random();
            while (passwordString.Length < 12)
            {
                if (rng.Next(2) == 1)
                    passwordString += Alphabet[rng.Next(Alphabet.Length)];
                else
                    passwordString += SpecialChars[rng.Next(SpecialChars.Length)];
            }
            
            return passwordString;
        }

        public int GetPasswordStrength(string password)
        {
            int value;

            var regularCharCount = password.Count(x => Alphabet.Contains(x));
            if (regularCharCount >= 8)
                value = 40;
            else
                value = regularCharCount * 5;

            value += password.Count(x => SpecialChars.Contains(x)) * 10;

            value += password.Count(x => Numerics.Contains(x)) * 8;

            if (value > 100)
                value = 100;

            return value;
        }

        public char[] GetRestrictedChars() => RestrictedChars;

        public bool IsValid(string password) => !password.Any(x => RestrictedChars.Contains(x));
    }
}
