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
    internal sealed class WriteResourceExclusionListTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.WriteResourceExclusionListTask;

        public WriteResourceExclusionListTask(string filePath, bool add, IEnumerable<ExcludedResource> excludedResources)
            : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                var created = false;
                if (!File.Exists(filePath))
                {
                    using (var _ = File.Create(filePath)) { }
                    created = true;
                    if (!add)
                        return;
                }

                var resources = new List<ExcludedResource>();
                if (!created)
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length != 0 && new BinaryFormatter().Deserialize(fs) is List<ExcludedResource> deserialized)
                        {
                            resources = deserialized;
                        }
                    }
                }

                if (add)
                {
                    resources.AddRange(excludedResources);
                }
                else
                {
                    if (resources.Any())
                    {
                        resources.RemoveAll(x => excludedResources.Any(z => z.Equals(x)));
                    }
                }

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    new BinaryFormatter().Serialize(fs, resources.Distinct().ToList());
                }
            });
        }
    }
}