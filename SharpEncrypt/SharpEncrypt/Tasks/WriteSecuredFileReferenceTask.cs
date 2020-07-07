using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
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

        public WriteSecuredFileReferenceTask(string path, IEnumerable<FileDataGridItemModel> models, bool add)
            : base(ResourceType.File, path)
        {
            InnerTask = new Task(() => 
            {
                var created = false;
                if (!File.Exists(path))
                {
                    using (var _ = File.Create(path)) { }
                    created = true;
                    if (!add)
                        return;
                }

                var listOfModels = new List<FileDataGridItemModel>();
                if (!created)
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileDataGridItemModel> list)
                        {
                            listOfModels = list;
                        }
                    }
                }

                if (add)
                {
                    listOfModels.AddRange(models);
                }
                else
                {
                    if (listOfModels.Any())
                    {
                        listOfModels.RemoveAll(x => models.Any(z => z.Secured.Equals(x.Secured, StringComparison.InvariantCulture)));
                    }
                }
                
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, listOfModels.Distinct().ToList());
                }
            });
        }
    }
}