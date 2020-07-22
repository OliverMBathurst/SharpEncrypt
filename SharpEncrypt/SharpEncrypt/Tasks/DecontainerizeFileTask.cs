using FileGeneratorLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class DecontainerizeFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.DecontainerizeFileTask;

        public DecontainerizeFileTask(FileDataGridItemModel model, string password, bool removeAfter = false, bool openAfter = false) : base(ResourceType.File, model.Secured)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.DecontainerizeFile(model.Secured, password);
                
                var path = FileGeneratorHelper.GetValidFileNameForDirectory(
                    Path.GetDirectoryName(model.Secured),
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
    }
}
