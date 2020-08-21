using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using AesLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Aes_Tasks
{
    internal sealed class AesSavePasswordsTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.AesSavePasswordsTask;

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

                ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
            });
        }
    }
}