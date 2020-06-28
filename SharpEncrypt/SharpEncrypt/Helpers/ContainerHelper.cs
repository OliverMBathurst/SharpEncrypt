using AESLibrary;
using SecureEraseLibrary;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpEncrypt.Helpers
{
    internal static class ContainerHelper
    {
        public static bool ValidateContainer(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var guidBytes = Constants.GetGuidBytes();
            using (var fs = new FileStream(filePath, FileMode.Open))
            {                
                if (fs.Length < guidBytes.Length * 2)
                    return false;

                var buffer = new byte[guidBytes.Length];
                fs.Read(buffer, 0, buffer.Length);

                if (!guidBytes.SequenceEqual(buffer))
                    return false;
            }

            if (FindEndGUIDBytes(filePath, guidBytes.Length, guidBytes, out var keyEndBytesRead))
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    var lengthOfKey = keyEndBytesRead - guidBytes.Length;

                    fs.Read(new byte[guidBytes.Length], 0, guidBytes.Length); //read first 32 bytes (guid bytes)

                    //now read in the key
                    var buffer = new byte[lengthOfKey];
                    fs.Read(buffer, 0, buffer.Length);

                    using (var ms = new MemoryStream(buffer))
                    {
                        if (new BinaryFormatter().Deserialize(ms) is AESKey key)
                            return true;
                    }
                }
            }

            return false;
        }

        public static void Containerize(string filePath, byte[] encryptedKeyBytes, int bufferLength = 1024)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var destFilePath = $"{Path.GetFileName(filePath)}.seef";

            while (File.Exists(destFilePath))
                destFilePath += ".seef";

            using (var fs = new FileStream(destFilePath, FileMode.Create))
            {
                var guidBytes = Constants.GetGuidBytes();
                fs.Write(guidBytes, 0, guidBytes.Length);
                fs.Write(encryptedKeyBytes, 0, encryptedKeyBytes.Length);
                fs.Write(guidBytes, 0, guidBytes.Length);

                using (var ifs = new FileStream(filePath, FileMode.Open))
                {
                    var buffer = new byte[bufferLength];
                    int read;
                    while ((read = ifs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, read);
                    }
                }
            }

            SecureEraseHelper.SDeleteFileWipe(filePath);
        }

        public static void Containerize(string filePath, AESKey key, string password)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            AESHelper.EncryptFile(key, filePath);

            var keyBytes = AESHelper.PasswordEncrypt(key, password);
            Containerize(filePath, keyBytes);
        }

        private static bool FindEndGUIDBytes(string filePath, int bytesRead, byte[] guidBytes, out int startByte)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            startByte = 0;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                for (var c = 0; c < bytesRead; c++)
                    fs.ReadByte();

                var buff = new byte[guidBytes.Length - 1];
                var otherBytes = guidBytes.Skip(1);

                int read;
                while((read = fs.ReadByte()) != -1)
                {
                    bytesRead++;
                    if(read == guidBytes[0])
                    {
                        var pos = fs.Position;
                        fs.Read(buff, 0, buff.Length);
                        if (buff.SequenceEqual(otherBytes))
                        {
                            startByte = bytesRead - 1;
                            return true;
                        }
                        fs.Seek(pos, SeekOrigin.Begin);
                    }
                }
            }
            return false;
        }
    }
}