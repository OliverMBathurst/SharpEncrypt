using System;
using System.IO;

namespace OTPTool
{
    internal class OTPInstance
    {
        private readonly string[] arguments;

        public OTPInstance(string[] args) => arguments = args;

        public void Execute()
        {
            if (arguments.Length == 1 && (arguments[0] == Resources.HelpSwitch || arguments[0] == Resources.HelpShortSwitch))
            {
                Console.WriteLine(Resources.Usage);
            }
            else
            {
                string path = string.Empty, keyPath = string.Empty, genKeyOfFile = string.Empty;
                bool encrypt = false, genKey = false;
                var keySize = 0L;

                for (var i = 0; i + 1 < arguments.Length; i++)
                {
                    switch (arguments[i])
                    {
                        case "-path":
                            path = arguments[i + 1];
                            i++;
                            break;
                        case "-encrypt":
                            encrypt = true;
                            break;
                        case "-decrypt":
                            encrypt = false;
                            break;
                        case "-key":
                            keyPath = arguments[i + 1];
                            i++;
                            break;
                        case "-genKey":
                            genKey = true;
                            if (File.Exists(arguments[i + 1]))
                            {
                                genKeyOfFile = arguments[i + 1];
                                i++;
                            }
                            break;
                        case "-keySize" when int.TryParse(arguments[i + 1], out var size):
                            keySize = size;
                            i++;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, arguments[i], Resources.Usage));
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

        private void GenerateKey(string path, long keySize, string genKeyOfFile)
        {

        }

        private void Encrypt(string path, string key)
        {

        }

        private void Decrypt(string path, string key)
        {

        }
    }
}