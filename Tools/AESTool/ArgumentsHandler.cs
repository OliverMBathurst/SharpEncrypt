using System;
using System.Globalization;
using System.Resources;

namespace AesTool
{
    internal sealed class ArgumentsHandler
    {
        private readonly ResourceManager _resourceManager = new ResourceManager(typeof(Resources));
        private readonly string[] _arguments;

        public ArgumentsHandler(string[] args) => _arguments = args;

        public void Execute()
        {
            
            if ((_arguments.Length == 1 && (_arguments[0] == _resourceManager.GetString("HelpSwitch", CultureInfo.CurrentCulture) || _arguments[0] == _resourceManager.GetString("HelpShortSwitch", CultureInfo.CurrentCulture))) || _arguments.Length == 0)
            {
                Console.WriteLine(_resourceManager.GetString("Usage", CultureInfo.CurrentCulture));
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
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, _resourceManager.GetString("InvalidArg", CultureInfo.CurrentCulture) ?? string.Empty, _arguments[i], _resourceManager.GetString("Usage", CultureInfo.CurrentCulture)));
                    }
                }

                if (genKey)
                {
                    AesHelper.WriteNewKey(inputPath);
                }
                else
                {
                    if (!AesHelper.TryGetKey(keyPath, out var key)) throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, _resourceManager.GetString("NotAKey", CultureInfo.CurrentCulture) ?? string.Empty, keyPath));
                    {
                        if (encrypt)
                        {
                            if (string.IsNullOrEmpty(outputPath))
                            {
                                AesHelper.EncryptFile(key, inputPath);
                            }
                            else
                            {
                                AesHelper.EncryptFile(key, inputPath, outputPath);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(outputPath))
                            {
                                AesHelper.DecryptFile(key, inputPath);
                            }
                            else
                            {
                                AesHelper.DecryptFile(key, inputPath, outputPath);
                            }
                        }
                    }
                }
            }
        }
    }
}