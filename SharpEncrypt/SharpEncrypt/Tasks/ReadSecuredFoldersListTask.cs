using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReadSecuredFoldersListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadSecuredFoldersListTask;

        public override SharpEncryptTaskResult Result { get; } = new SharpEncryptTaskResult { Type = typeof(IEnumerable<string>) };

        public ReadSecuredFoldersListTask(string filePath)
        {
            InnerTask = new Task(() => 
            {
                var filesList = new List<string>();
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if(new BinaryFormatter().Deserialize(fs) is List<string> paths)
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