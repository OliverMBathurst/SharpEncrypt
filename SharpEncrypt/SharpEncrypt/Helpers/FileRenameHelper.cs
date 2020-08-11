using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpEncrypt.Helpers
{
    internal static class FileRenameHelper
    {
        public static void AnonymizeFile(string filePath, string hash)
        {
            var bytes = Encoding.UTF8.GetBytes(Path.GetFileNameWithoutExtension(filePath));
            var hashBytes = Encoding.UTF8.GetBytes(hash).ToList();

            if (bytes.Length > hashBytes.Count)
            {
                PadHashByteArray(hashBytes, bytes.Length - hashBytes.Count);
            }

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ hashBytes[i]);
            }
            
            var destFileName = Path.Combine(DirectoryHelper.GetDirectoryPath(filePath), $"{BitConverter.ToString(bytes).Replace("-", string.Empty)}{Path.GetExtension(filePath)}");
            File.Move(filePath, destFileName);
        }

        public static void DeanonymizeFile(string filePath, string hash)
        {
            var hashBytes = Encoding.UTF8.GetBytes(hash).ToList();
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            var bytes = new List<byte>();
            for (var i = 1; i < fileName.Length; i += 2)
            {
                bytes.Add(Convert.ToByte($"{fileName[i - 1]}{fileName[i]}", 16));
            }

            if (bytes.Count > hashBytes.Count)
            {
                PadHashByteArray(hashBytes, bytes.Count - hashBytes.Count);
            }

            for (var j = 0; j < bytes.Count; j++)
            {
                bytes[j] = (byte)(bytes[j] ^ hashBytes[j]);
            }

            var destFileName = Path.Combine(DirectoryHelper.GetDirectoryPath(filePath), $"{string.Join(string.Empty, bytes.Select(Convert.ToChar))}{Path.GetExtension(filePath)}");
            File.Move(filePath, destFileName);
        }

        private static void PadHashByteArray(List<byte> hashBytes, int remaining)
        {
            while (remaining > 0)
            {
                if (remaining < hashBytes.Count)
                {
                    hashBytes = hashBytes.Concat(hashBytes.Take(remaining)).ToList();
                    remaining = 0;
                }
                else
                {
                    var take = hashBytes.Count;
                    hashBytes = hashBytes.Concat(hashBytes.Take(take)).ToList();
                    remaining -= take;
                }
            }
        }
    }
}