using ExtensionMethods.CharExtensionMethods;
using ExtensionMethods.DateTimeExtensionMethods;
using ExtensionMethods.EnumerableExtensionMethods;
using System;
using System.IO;
using System.Linq;

namespace FileGen
{
    class FileGen
    {
        static void Main(string[] args)
            => Write(GetPath(args), GetSize(args), Random(args), Pause(args));

        private static string GetPath(string[] args)
        {
            var defPath = Path.GetPathRoot(Environment.CurrentDirectory);
            var pathIndex = args.IndexOf("-path");
            if(pathIndex != -1 && pathIndex + 1 < args.Length)
                return args[pathIndex + 1];

            return defPath;
        }

        private static long GetSize(string[] args)
        {
            var sizeIndex = args.IndexOf("-size");
            if (sizeIndex != -1 && sizeIndex + 1 < args.Length)
                if (long.TryParse(args[sizeIndex + 1], out var longSize))
                    return longSize;
            return -1L;
        }

        private static bool Random(string[] args)
        {
            var randomIndex = args.IndexOf("-random");
            if(randomIndex != -1 && randomIndex + 1 < args.Length)
                if(bool.TryParse(args[randomIndex + 1], out var random))
                    return random;
            return false;
        }

        private static bool Pause(string[] args)
        {
            var pauseIndex = args.IndexOf("-pause");
            if(pauseIndex != -1 && pauseIndex + 1 < args.Length)
                if(bool.TryParse(args[pauseIndex + 1], out var pauseResult))
                    return pauseResult;
            return true;
        }        

        private static void Write(string path, long size, bool random, bool pause)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException($"{path} is not a directory.");

            var drive = DriveInfo.GetDrives().First(x => x.Name[0].ToLower() == path[0].ToLower());
            Console.WriteLine(drive);

            if (size == -1L)
                size = drive.AvailableFreeSpace;

            var provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var genFilePath = $"{path}\'fileGen_{new DateTime().ToUnixTime()}.BIN";

            Console.WriteLine($"Writing to: {genFilePath}");
            using (var sw = new StreamWriter(File.Create(genFilePath)))
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

            if (pause)
            {
                Console.WriteLine("The file has been generated, press any key to continue...");
                Console.ReadLine();
            }                
        }
    }
}