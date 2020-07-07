using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReadSecuredFoldersListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSecuredFoldersListTask;

        public ReadSecuredFoldersListTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() => 
            {
                var filesList = new List<string>();
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<string> paths)
                        {
                            filesList = paths;
                        }
                    }
                }

                Result.Value = filesList;
            });
        }
    }
}