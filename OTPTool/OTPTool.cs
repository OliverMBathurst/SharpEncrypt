using System;
using System.IO;
using System.Security.Cryptography;

namespace OTPTool
{
    internal sealed class OTPTool
    {
        private readonly string[] _arguments;

        public OTPTool(string[] args) => _arguments = args;

        public void Execute()
        {
            if ((_arguments.Length == 1 && (_arguments[0] == Resources.HelpSwitch || _arguments[0] == Resources.HelpShortSwitch)) || _arguments.Length == 0)
            {
                Console.WriteLine(Resources.Usage);
            }
            else
            {
                string path = string.Empty, keyPath = string.Empty, genKeyOfFile = string.Empty;
                bool encrypt = false, genKey = false;
                var keySize = 0L;

                for (var i = 0; i < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path" when i + 1 < _arguments.Length:
                            path = _arguments[i + 1];
                            i++;
                            break;
                        case "-encrypt":
                            encrypt = true;
                            break;
                        case "-decrypt":
                            encrypt = false;
                            break;
                        case "-key" when i + 1 < _arguments.Length:
                            keyPath = _arguments[i + 1];
                            i++;
                            break;
                        case "-genKey":
                            genKey = true;
                            if (i + 1 < _arguments.Length && File.Exists(_arguments[i + 1]))
                            {
                                genKeyOfFile = _arguments[i + 1];
                                i++;
                            }
                            break;
                        case "-keySize" when i + 1 < _arguments.Length && int.TryParse(_arguments[i + 1], out var size):
                            keySize = size;
                            i++;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, _arguments[i], Resources.Usage));
                    }
                }

                if (genKey)
                    GenerateKey(path, keySize, genKeyOfFile);
                else
                    if (encrypt)
                        Encrypt(path, keyPath);
                    else
                        Decrypt(path, keyPath);
            }
        }

        //put this in a library
        private void GenerateKey(string path, long keySize, string genKeyOfFile)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            var keyLength = keySize;
            if (genKeyOfFile != string.Empty)
            {
                if (File.Exists(genKeyOfFile))
                {
                    keyLength = new FileInfo(genKeyOfFile).Length;
                }
                else
                {
                    throw new ArgumentException($"{genKeyOfFile} does not exist.");
                }
            }

            if (!File.Exists(path))
            {
                using (var provider = new RNGCryptoServiceProvider()) {
                    using (var fs = new FileStream(path, FileMode.CreateNew))
                    {
                        var byteArray = new byte[1024];
                        var length = keyLength;
                        while (length > 0)
                        {
                            if (length < 1024)
                                byteArray = new byte[length];

                            provider.GetNonZeroBytes(byteArray);
                            fs.Write(byteArray, 0, byteArray.Length);
                            length -= byteArray.Length;
                        }
                    }
                }
            }
            else
            {
                throw new IOException($"{path} already exists.");
            }
        }

        private void Encrypt(string path, string key)
        {

        }

        private void Decrypt(string path, string key)
        {

        }
    }
}