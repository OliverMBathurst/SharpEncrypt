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
    internal sealed class WriteSecuredFileReference : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteSecuredFileReference;

        public override SharpEncryptTaskResult Result { get; } = new SharpEncryptTaskResult { Type = typeof(bool) };

        public WriteSecuredFileReference(string path, IEnumerable<FileDataGridItemModel> models, bool add = true)
        {
            InnerTask = new Task(() => 
            {
                var listOfModels = models.ToList();
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {                        
                        if (new BinaryFormatter().Deserialize(fs) is List<FileDataGridItemModel> list)
                        {
                            listOfModels = list;
                            foreach (var model in models)
                            {
                                if (add)
                                    listOfModels.Add(model);
                                else
                                    listOfModels.Remove(model);
                            }
                        }
                    }
                }

                using (var f = new FileStream(path, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(f, listOfModels);
                }
            });
        }
    }
}