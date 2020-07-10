using System.IO;

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
    }
}
