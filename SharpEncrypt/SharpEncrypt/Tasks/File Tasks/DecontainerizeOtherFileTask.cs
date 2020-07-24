using System.IO;
using System.Threading.Tasks;
using FileGeneratorLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class DecontainerizeOtherFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeOtherFileTask;

        public DecontainerizeOtherFileTask(string path, string password) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.DecontainerizeFile(path, password);

                var newFilePath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    Path.GetDirectoryName(path),
                    Path.GetFileNameWithoutExtension(path),
                    string.Empty);

                File.Move(path, newFilePath);
            });
        }
    }
}
