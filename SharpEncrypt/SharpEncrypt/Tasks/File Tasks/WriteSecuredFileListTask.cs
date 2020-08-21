using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class WriteSecuredFileListTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.WriteSecuredFileListTask;

        public WriteSecuredFileListTask(string path, params string[] paths) : base(ResourceType.File, path)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(path))
                    return;

                var listOfModels = new List<FileModel>();
                
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileModel> list)
                    {
                        listOfModels = list;
                    }
                }
                
                if (listOfModels.Any())
                    listOfModels.RemoveAll(x => paths.Contains(x.Secured));

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, listOfModels.Distinct().ToList());
                }
            });
        }

        public WriteSecuredFileListTask(string path, bool add, params FileModel[] models)
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

                var listOfModels = new List<FileModel>();
                if (!created)
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<FileModel> list)
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
                        listOfModels.RemoveAll(x => models.Any(z => z.Secured.Equals(x.Secured, StringComparison.Ordinal)));
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