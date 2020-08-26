using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.Models;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    public sealed class OpenOtpPasswordStoreTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.OpenOtpPasswordStoreTask;

        public OpenOtpPasswordStoreTask(string filePath, string keyFilePath) 
            : base(ResourceType.File, filePath, keyFilePath)
        {
            InnerTask = new Task(() =>
            {
                var model = new OpenOtpPasswordStoreTaskResultModel 
                { 
                    StorePath = filePath,
                    KeyPath = keyFilePath                
                };

                if (!File.Exists(filePath))
                {
                    using (var fs = File.Create(filePath)) 
                    {
                        new BinaryFormatter().Serialize(fs, new List<PasswordModel>());
                    }
                    model.Exception = new OtpPasswordStoreFirstUseException();
                    Result.Value = model;
                    return;
                }

                if (!File.Exists(keyFilePath))
                {
                    model.Exception = new OtpKeyFileNotAvailableException();
                    Result.Value = model;
                    return;
                }

                if (new FileInfo(filePath).Length > new FileInfo(keyFilePath).Length)
                {
                    throw new InvalidKeyException();
                }

                OtpHelper.Transform(filePath, keyFilePath);

                var models = new List<PasswordModel>();
                using(var fs = new FileStream(filePath, FileMode.Open))
                {
                    if (fs.Length > 0)
                    {
                        if (new BinaryFormatter().Deserialize(fs) is List<PasswordModel> list)
                        {
                            models = list;
                        }
                    }
                }

                OtpHelper.Transform(filePath, keyFilePath);

                model.Models = models;
                Result.Value = model;
            });
        }
    }
}
