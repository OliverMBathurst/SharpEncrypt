using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReadResourceExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReadResourceExclusionListTask;

        public ReadResourceExclusionListTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<ExcludedResource> resources)
                        {
                            Result.Value = resources;
                        }
                    }
                }
            });
        }
    }
}