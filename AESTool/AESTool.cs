using AESLibrary;
using SecureEraseLibrary;
using System;
using System.IO;

namespace AESTool
{
    internal class AESTool
    {
        private readonly string[] _arguments;
        private readonly AESInstance _aesInstance = new AESInstance();
        private readonly SecureEraseInstance _secureEraseInstance = new SecureEraseInstance();

        public AESTool(string[] args) => _arguments = args;

        public void Execute()
        {
            if ((_arguments.Length == 1 && (_arguments[0] == Resources.HelpSwitch || _arguments[0] == Resources.HelpShortSwitch)) || _arguments.Length == 0)
            {
                Console.WriteLine(Resources.Usage);
            }
            else
            {
                string inputPath = string.Empty, keyPath = string.Empty;
                bool encrypt = true, genKey = false;
                int keySize = 256, blockSize = 128;

                for (var i = 0; i < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path" when i + 1 < _arguments.Length:
                            inputPath = _arguments[i + 1];
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
                        case "-keySize" when i + 1 < _arguments.Length && int.TryParse(_arguments[i + 1], out var size):
                            keySize = size;
                            i++;
                            break;
                        case "-blockSize" when i + 1 < _arguments.Length && int.TryParse(_arguments[i + 1], out var size):
                            blockSize = size;
                            i++;
                            break;
                        default:
                            throw new ArgumentException(string.Format(Resources.InvalidArg, _arguments[i], Resources.Usage));
                    }
                }

                if (genKey)
                {
                    _aesInstance.WriteNewKey(inputPath, keySize, blockSize);
                }
                else
                {
                    if (_aesInstance.TryGetKey(keyPath, out var key))
                    {
                        var fileName = Path.GetFileName(inputPath);
                        var ext = Path.GetExtension(inputPath);

                        var outputPath = _secureEraseInstance.CreateUniqueFileForDirectory(Path.GetDirectoryName(inputPath), ext);

                        if (encrypt)
                            _aesInstance.Encrypt(key, inputPath, outputPath);
                        else
                            _aesInstance.Decrypt(key, inputPath, outputPath);

                        _secureEraseInstance.ObfuscateFileProperties(inputPath);
                        _secureEraseInstance.WriteRandomData(inputPath);
                        File.Delete(_secureEraseInstance.ObfuscateFileName(inputPath));
                        File.Move(outputPath, fileName);
                    }
                    else
                    {
                        throw new ArgumentException($"{keyPath} is not a valid key.");
                    }
                }
            }
        }
    }
}