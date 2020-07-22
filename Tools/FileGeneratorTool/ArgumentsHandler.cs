using System;
using System.Globalization;
using System.IO;
using System.Resources;

namespace FileGeneratorTool
{
    internal sealed class ArgumentsHandler
    {
        private readonly ResourceManager _resourceManager = new ResourceManager(typeof(Resources));
        private readonly string[] _arguments;

        public ArgumentsHandler(string[] args) => _arguments = args;

        public void Execute()
        {
            var path = Environment.CurrentDirectory;
            var length = 0L;
            bool random = false, pause = true, postDelete = true;

            if ((_arguments.Length == 1 && (_arguments[0] == _resourceManager.GetString("HelpSwitch", CultureInfo.CurrentCulture) || _arguments[0] == _resourceManager.GetString("HelpShortSwitch", CultureInfo.CurrentCulture))) || _arguments.Length == 0)
            {
                Console.WriteLine(_resourceManager.GetString("Usage", CultureInfo.CurrentCulture));
            }
            else
            {
                for (var i = 0; i + 1 < _arguments.Length; i++)
                {
                    switch (_arguments[i])
                    {
                        case "-path" when Directory.Exists(_arguments[i + 1]):
                            path = _arguments[i + 1];
                            break;
                        case "-length" when long.TryParse(_arguments[i + 1], out var lengthResult):
                            length = lengthResult;
                            break;
                        case "-random" when bool.TryParse(_arguments[i + 1], out var randomResult):
                            random = randomResult;
                            break;
                        case "-pause" when bool.TryParse(_arguments[i + 1], out var pauseResult):
                            pause = pauseResult;
                            break;
                        case "-postDelete" when bool.TryParse(_arguments[i + 1], out var postResult):
                            postDelete = postResult;
                            break;
                        default:
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, _resourceManager.GetString("InvalidArg", CultureInfo.CurrentCulture), _arguments[i], _resourceManager.GetString("Usage", CultureInfo.CurrentCulture)));
                    }
                    i++;
                }

                FileGeneratorHelper.WriteNewFile(path, length, random, postDelete);

                if (!pause) return;
                Console.WriteLine(_resourceManager.GetString("FileHasBeenGenerated", CultureInfo.CurrentCulture));
                Console.ReadLine();
            }
        }
    }
}