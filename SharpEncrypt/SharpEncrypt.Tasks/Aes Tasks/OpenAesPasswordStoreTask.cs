using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using AesLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.Aes_Tasks
{
    public sealed class OpenAesPasswordStoreTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.OpenAesPasswordStoreTask;

        public OpenAesPasswordStoreTask(string filePath, string password) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(filePath))
                    ContainerizeNewAesPasswordStore(filePath);

                if (ContainerHelper.ValidateContainer(filePath, password))
                {
                    ContainerHelper.DecontainerizeFile(filePath, password);
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        if (fs.Length > 0 && new BinaryFormatter().Deserialize(fs) is List<PasswordModel> models)
                        {
                            Result.Value = new OpenAesPasswordStoreTaskResultModel(models);
                        }
                        else
                        {
                            ContainerizeNewAesPasswordStore(filePath);
                        }
                    }

                    ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
                }
                else
                {
                    ContainerizeNewAesPasswordStore(filePath);
                }

                void ContainerizeNewAesPasswordStore(string path)
                {
                    using (var fs = new FileStream(path, FileMode.Create)) 
                    {
                        new BinaryFormatter().Serialize(fs, new List<PasswordModel>());
                    }
                    ContainerHelper.ContainerizeFile(path, AesHelper.GetNewAesKey(), password);
                    Result.Value = new OpenAesPasswordStoreTaskResultModel(new List<PasswordModel>());
                    return;
                }
            });
        }
    }
}