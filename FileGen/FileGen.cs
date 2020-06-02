using ExtensionMethods.CharExtensionMethods;
using ExtensionMethods.DateTimeExtensionMethods;
using System;
using System.IO;
using System.Linq;

namespace FileGen
{
    internal class FileGen
    {
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory;
            var size = -1L;
            bool random = false, pause = true, postDelete = true;

            if(args.Length == 1 && (args[0] == "-help" || args[0] == "-h"))
            {
                Usage();
            }
            else
            {
                for (var i = 0; i + 1 < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-path":
                            path = args[i + 1];
                            break;
                        case "-size" when long.TryParse(args[i + 1], out var sizeResult):
                            size = sizeResult;
                            break;
                        case "-random" when bool.TryParse(args[i + 1], out var randomResult):
                            random = randomResult;
                            break;
                        case "-pause" when bool.TryParse(args[i + 1], out var pauseResult):
                            pause = pauseResult;
                            break;
                        case "-postDelete" when bool.TryParse(args[i + 1], out var postResult):
                            postDelete = postResult;
                            break;
                        default:
                            throw new ArgumentException($"Invalid argument: {args[i]}\n{Usage()}");
                    }
                    i++;
                }

                Write(path, size, random, pause, postDelete);
            }
        }

        private static string Usage() => "Usage: [-path] {true|false} [-size] {number} [-random] {true|false} [-pause] {true|false} [-postDelete] {true|false}";

        private static void Write(string path, long size, bool random, bool pause, bool postDelete)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException($"{path} is not a directory.");

            var drive = DriveInfo.GetDrives().First(x => x.Name[0].ToLower() == path[0].ToLower());

            if (size == -1L)
                size = drive.AvailableFreeSpace;

            var provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var genFilePath = $"{path}\\fileGen_{DateTime.Now.ToUnixTime()}.BIN";

            Console.WriteLine($"Writing to: {genFilePath}");
            using (var sw = new BinaryWriter(File.Create(genFilePath)))
            {
                var completed = false;
                while (!completed)
                {
                    var writeSize = 1024L;
                    if (size < writeSize)
                        writeSize = size;

                    var bytes = new byte[writeSize];
                    if (random)
                        provider.GetBytes(bytes);
                    sw.Write(bytes);

                    Console.WriteLine($"Wrote {writeSize} bytes");

                    size -= writeSize;

                    if (size == 0L)
                        completed = true;
                }
            }

            if (postDelete)
                File.Delete(genFilePath);

            if (pause)
            {
                Console.WriteLine("The file has been generated, press enter to continue...");
                Console.ReadLine();
            }                
        }
    }
}