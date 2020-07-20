using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharpEncrypt.ExtensionClasses
{
    public static class StringExtensions
    {
        public static bool Exists(this string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
                return true;
            return false;
        }

        public static string Hash(this string str)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                return string.Join(string.Empty, hash.Select(x => string.Format(CultureInfo.InvariantCulture, "{0:x2}", x)));
            }
        }

        public static string RemoveLast(this string str, int number)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            if (number >= str.Length)
                return string.Empty;

            return str.Substring(0, str.Length - number);
        }
    }
}
