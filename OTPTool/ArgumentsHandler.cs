using OTPLibrary;
using System;
using System.IO;

namespace OTPTool
{
    internal sealed class ArgumentsHandler
    {
        private readonly string[] _arguments;

        public ArgumentsHandler(string[] args) => _arguments = args;

        public void Execute()
        {
            if ((_arguments.Length == 1 && (_arguments[0] == Resources.HelpSwitch || _arguments[0] == Resources.HelpShortSwitch)) || _arguments.Length == 0)
            {
                Console.WriteLine(Resources.Usage);
            }
            else
            {
                string path = string.Empty, keyPath = string.Empty, referenceFile = string.Empty;
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
                                referenceFile = _arguments[i + 1];
                                i++;
                            }
                            break;
                        case "-keySize" when i + 1 < _arguments.Length && long.TryParse(_arguments[i + 1], out var size):
                            keySize = size;
                            i++;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, _arguments[i], Resources.Usage));
                    }
                }

                if (genKey)
                {
                    if (string.IsNullOrEmpty(referenceFile))
                        OTPHelper.GenerateKey(path, keySize);
                    else
                        OTPHelper.GenerateKey(path, referenceFile);
                }
                else
                {
                    if (encrypt)
                        OTPHelper.Encrypt(path, keyPath);
                    else
                        OTPHelper.Decrypt(path, keyPath);
                }
            }
        }
    }
}