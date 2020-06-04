using SecureEraseLibrary;
using System;

namespace SecureShred
{
    internal sealed class SecureShred
    {
        private readonly SecureEraseInstance _secureShredInstance = new SecureEraseInstance();
        private readonly string[] _arguments;

        public SecureShred(string[] args) => _arguments = args;

        public void Execute()
        {
            if ((_arguments.Length == 1 && (_arguments[0] == Resources.HelpSwitch || _arguments[0] == Resources.HelpShortSwitch)) || _arguments.Length == 0)
            {
                Console.WriteLine(Resources.Usage);
            }
            else
            {
                var path = string.Empty;
                var cipher = CipherType.OTP;
                var shredType = Resources.File;
                bool recurse = false, nameObfuscation = true, propertyObfuscation = true;

                for (var i = 0; i + 1 < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path":
                            path = _arguments[i + 1];
                            break;
                        case "-cipher" when Enum.TryParse(_arguments[i + 1], out CipherType cipherResult):
                            cipher = cipherResult;
                            break;
                        case "-type":
                            shredType = _arguments[i + 1];
                            break;
                        case "-recurse" when bool.TryParse(_arguments[i + 1], out var recurseResult):
                            recurse = recurseResult;
                            break;
                        case "-nameObfuscation" when bool.TryParse(_arguments[i + 1], out var nameObfuscationResult):
                            nameObfuscation = nameObfuscationResult;
                            break;
                        case "-propertyObfuscation" when bool.TryParse(_arguments[i + 1], out var propertyObfuscationResult):
                            propertyObfuscation = propertyObfuscationResult;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, _arguments[i], Resources.Usage));
                    }
                    i++;
                }

                if(shredType.ToLower() == Resources.File.ToLower())
                {
                    _secureShredInstance.ShredFile(path, cipher, nameObfuscation, propertyObfuscation);
                }
                else
                {
                    _secureShredInstance.ShredDirectory(path, cipher, recurse, nameObfuscation, propertyObfuscation);
                }
            }
        }
    }
}