using System.IO;
using System.Threading.Tasks;
using FileGeneratorLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class DecontainerizeFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeFileTask;

        public DecontainerizeFileTask(FileModel model, string password, bool removeAfter = false, bool openAfter = false) : base(ResourceType.File, model.Secured)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.DecontainerizeFile(model.Secured, password);
                
                var path = FileGeneratorHelper.GetValidFileNameForDirectory(
                    DirectoryHelper.GetDirectoryPath(model.Secured),
                    Path.GetFileNameWithoutExtension(model.File),
                    Path.GetExtension(model.File));

                File.Move(model.Secured, path);

                Result.Value = new DecontainerizeFileTaskResult 
                {
                    Model = model,
                    NewPath = path,
                    DeleteAfter = removeAfter, 
                    OpenAfter = openAfter 
                };
            });
        }

        public DecontainerizeFileTask(string filePath, string password, bool removeAfter = false, bool openAfter = false) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.DecontainerizeFile(filePath, password);

                var path = FileGeneratorHelper.GetValidFileNameForDirectory(
                    DirectoryHelper.GetDirectoryPath(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    string.Empty);

                File.Move(filePath, path);

                Result.Value = new DecontainerizeFileTaskResult
                {
                    Model = path.ToFileModel(),
                    NewPath = path,
                    DeleteAfter = removeAfter,
                    OpenAfter = openAfter
                };
            });
        }
    }
}