using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace SharpEncrypt
{
    internal sealed class RSAKeyWriter
    {
        public void Write(string path, RSAParameters parameters)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, parameters);
            }
        }
    }
}
