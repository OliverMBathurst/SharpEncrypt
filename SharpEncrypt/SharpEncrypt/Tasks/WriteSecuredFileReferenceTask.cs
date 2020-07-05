using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class WriteSecuredFileReferenceTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSecuredFileReferenceTask;

        public WriteSecuredFileReferenceTask(string path, IEnumerable<FileDataGridItemModel> models, bool add = true)
            : base(ResourceType.File, path)
        {
            InnerTask = new Task(() => 
            {
                var listOfModels = GetModels(path);

                foreach (var model in models)
                {
                    if (add)
                        listOfModels.Add(model);
                    else
                        listOfModels.RemoveAll(x => x.Secured == model.Secured);
                }

                WriteModelsToFile(path, listOfModels);
            });
        }

        public WriteSecuredFileReferenceTask(string path, IEnumerable<string> securedFilePaths)
            : base(ResourceType.File, path)
        {
            InnerTask = new Task(() => 
            {
                var listOfModels = GetModels(path)
                    .Where(x => !securedFilePaths.Any(z => z == x.Secured))
                    .ToList();

                WriteModelsToFile(path, listOfModels);
            });
        }

        private static List<FileDataGridItemModel> GetModels(string path)
        {
            if (File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    if (new BinaryFormatter().Deserialize(fs) is List<FileDataGridItemModel> list)
                    {
                        return list;
                    }
                }
            }
            return new List<FileDataGridItemModel>();
        }

        private static void WriteModelsToFile(string path, List<FileDataGridItemModel> models)
        {
            using (var f = new FileStream(path, FileMode.Create))
            {
                new BinaryFormatter().Serialize(f, models);
            }
        }
    }
}