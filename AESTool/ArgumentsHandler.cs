using AESLibrary;
using SecureEraseLibrary;
using System;
using System.IO;

namespace AESTool
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
                string inputPath = string.Empty, outputPath = string.Empty, keyPath = string.Empty;
                bool encrypt = true, genKey = false;

                for (var i = 0; i < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path" when i + 1 < _arguments.Length:
                            inputPath = _arguments[i + 1];
                            i++;
                            break;
                        case "-outputPath" when i + 1 < _arguments.Length:
                            outputPath = _arguments[i + 1];
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
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, _arguments[i], Resources.Usage));
                    }
                }

                if (genKey)
                {
                    AESHelper.WriteNewKey(inputPath);
                }
                else
                {
                    if (!AESHelper.TryGetKey(keyPath, out var key)) throw new ArgumentException(string.Format(Resources.NotAKey, keyPath));
                    {
                        var secureEraseInstance = new SecureEraseHelper();
                        if (string.IsNullOrEmpty(outputPath))
                        {
                            if (encrypt)
                                AESHelper.EncryptFile(key, inputPath);
                            else
                                AESHelper.DecryptFile(key, inputPath);
                        }
                        else
                        {
                            var fileName = Path.GetFileName(inputPath);

                            if (encrypt)
                                AESHelper.EncryptFile(key, inputPath, outputPath);
                            else
                                AESHelper.EncryptFile(key, inputPath, outputPath);

                            SecureEraseHelper.ObfuscateFileProperties(inputPath);
                            SecureEraseHelper.WriteRandomData(inputPath);
                            File.Delete(SecureEraseHelper.ObfuscateFileName(inputPath));
                            File.Move(outputPath, fileName);
                        }
                    }
                }
            }
        }
    }
}