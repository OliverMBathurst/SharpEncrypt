using AESLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class AesSavePasswordsTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.AESSavePasswordsTask;

        public AesSavePasswordsTask(string filePath, string password, List<PasswordModel> models) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (ContainerHelper.ValidateContainer(filePath, password))
                {
                    ContainerHelper.DecontainerizeFile(filePath, password);
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (new BinaryFormatter().Deserialize(fs) is List<PasswordModel> existingModels)
                        {
                            models.AddRange(existingModels);
                        }
                    }
                }

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, models);
                }

                ContainerHelper.ContainerizeFile(filePath, AESHelper.GetNewAESKey(), password);
            });
        }
    }
}