using System.Text;

namespace SharpEncrypt.ExtensionClasses
{
    public static class CharExtensions
    {
        public static string Repeat(this char c, int times)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < times; i++)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
