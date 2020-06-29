using OTPLibrary;
using System;
using System.Globalization;
using System.IO;
using System.Resources;

namespace OTPTool
{
    internal sealed class ArgumentsHandler
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));
        private readonly string[] _arguments;

        public ArgumentsHandler(string[] args) => _arguments = args;

        public void Execute()
        {
            if ((_arguments.Length == 1 && (_arguments[0] == ResourceManager.GetString("HelpSwitch", CultureInfo.CurrentCulture) || _arguments[0] == ResourceManager.GetString("HelpShortSwitch", CultureInfo.CurrentCulture))) || _arguments.Length == 0)
            {
                Console.WriteLine(ResourceManager.GetString("Usage", CultureInfo.CurrentCulture));
            }
            else
            {
                string path = string.Empty, keyPath = string.Empty, referenceFile = string.Empty;
                var genKey = false;
                var keySize = 0L;

                for (var i = 0; i < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path" when i + 1 < _arguments.Length:
                            path = _arguments[i + 1];
                            i++;
                            break;
                        case "-keyPath" when i + 1 < _arguments.Length:
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
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("InvalidArg", CultureInfo.CurrentCulture), _arguments[i], ResourceManager.GetString("Usage", CultureInfo.CurrentCulture)));
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
                    OTPHelper.Transform(path, keyPath);
                }
            }
        }
    }
}