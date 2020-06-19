using SecureEraseLibrary;
using System;
using System.Globalization;
using System.Resources;

namespace SecureShred
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
                var path = string.Empty;
                var cipher = CipherType.OTP;
                var shredType = string.Empty;
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
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("InvalidArg", CultureInfo.CurrentCulture), _arguments[i], ResourceManager.GetString("Usage", CultureInfo.CurrentCulture)));
                    }
                    i++;
                }
                
                if (shredType.ToLower(CultureInfo.CurrentCulture) == ResourceManager.GetString("File", CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture))
                    SecureEraseHelper.ShredFile(path, cipher, nameObfuscation, propertyObfuscation);
                else if(shredType.ToLower(CultureInfo.CurrentCulture) == ResourceManager.GetString("Directory", CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture))
                    SecureEraseHelper.ShredDirectory(path, cipher, recurse, nameObfuscation, propertyObfuscation);
                else
                    throw new ArgumentException(ResourceManager.GetString("InvalidShredType", CultureInfo.CurrentCulture));
            }
        }
    }
}