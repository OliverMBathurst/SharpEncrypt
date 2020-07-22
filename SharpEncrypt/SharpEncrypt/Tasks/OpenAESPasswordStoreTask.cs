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
    internal sealed class OpenAesPasswordStoreTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OpenAESPasswordStoreTask;

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
                            Result.Value = new OpenAesPasswordStoreTaskResult(models);
                        }
                        else
                        {
                            ContainerizeNewAesPasswordStore(filePath);
                        }
                    }

                    ContainerHelper.ContainerizeFile(filePath, AESHelper.GetNewAESKey(), password);
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
                    ContainerHelper.ContainerizeFile(path, AESHelper.GetNewAESKey(), password);
                    Result.Value = new OpenAesPasswordStoreTaskResult(new List<PasswordModel>());
                    return;
                }
            });
        }
    }
}