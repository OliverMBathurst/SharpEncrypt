using FileGeneratorLibrary;
using System;
using System.Globalization;
using System.IO;
using System.Resources;

namespace FileGeneratorTool
{
    internal sealed class ArgumentsHandler
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));
        private readonly string[] _arguments;

        public ArgumentsHandler(string[] args) => _arguments = args;

        public void Execute()
        {
            var path = Environment.CurrentDirectory;
            var length = -1L;
            bool random = false, pause = true, postDelete = true;

            if ((_arguments.Length == 1 && (_arguments[0] == ResourceManager.GetString("HelpSwitch", CultureInfo.CurrentCulture) || _arguments[0] == ResourceManager.GetString("HelpShortSwitch", CultureInfo.CurrentCulture))) || _arguments.Length == 0)
            {
                Console.WriteLine(ResourceManager.GetString("Usage", CultureInfo.CurrentCulture));
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
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("InvalidArg", CultureInfo.CurrentCulture), _arguments[i], ResourceManager.GetString("Usage", CultureInfo.CurrentCulture)));
                    }
                    i++;
                }

                FileGeneratorHelper.WriteNewFile(path, length, random, postDelete);

                if (pause)
                {
                    Console.WriteLine(ResourceManager.GetString("FileHasBeenGenerated", CultureInfo.CurrentCulture));
                    Console.ReadLine();
                }
            }
        }
    }
}