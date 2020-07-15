using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpEncrypt.Helpers
{
    internal static class AnonymousRenameHelper
    {
        public static void AnonymiseFile(string filePath, string hash)
            => AnonymousRename(filePath, hash);

        public static void DeanonymiseFile(string filePath, string hash)
            => Deanonymise(filePath, hash);

        private static void AnonymousRename(string filePath, string hash) 
        {
            var dir = Path.GetDirectoryName(filePath);
            var ext = Path.GetExtension(filePath);

            var bytes = Encoding.UTF8.GetBytes(Path.GetFileNameWithoutExtension(filePath));
            var hashBytes = Encoding.UTF8.GetBytes(hash);

            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < hashBytes.Length)
                {
                    bytes[i] = (byte)(bytes[i] ^ hashBytes[i]);
                }
            }

            var destFileName = 
                $"{dir}" + 
                @"\" + 
                $"{BitConverter.ToString(bytes).Replace("-", string.Empty)}{ext}";

            File.Move(filePath, destFileName);
        }

        private static void Deanonymise(string filePath, string hash)
        {
            var dir = Path.GetDirectoryName(filePath);
            var ext = Path.GetExtension(filePath);
            var hashBytes = Encoding.UTF8.GetBytes(hash);

            var fileName = Path.GetFileNameWithoutExtension(filePath);

            var bytes = new List<byte>();
            for (var i = 1; i < fileName.Length; i += 2)
            {
                bytes.Add(Convert.ToByte($"{fileName[i - 1]}{fileName[i]}", 16));
            }

            for (var j = 0; j < bytes.Count; j++)
            {
                if (j < hashBytes.Length)
                {
                    bytes[j] = (byte)(bytes[j] ^ hashBytes[j]); 
                }
            }

            var destFileName = 
                $"{dir}" + 
                @"\" + 
                $"{string.Join(string.Empty, bytes.Select(x => Convert.ToChar(x)))}{ext}";
            File.Move(filePath, destFileName);
        }
    }
}