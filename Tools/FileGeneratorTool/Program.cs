
namespace FileGeneratorTool
{
    internal sealed class Program
    {
        private static void Main(string[] args) => new ArgumentsHandler(args).Execute();
    }
}