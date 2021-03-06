﻿using SharpEncrypt.Models;
using SharpEncrypt.Enums;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FileGeneratorLibrary;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.Rsa_Tasks
{
    public sealed class BulkExportKeysTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.BulkExportKeysTask;

        public BulkExportKeysTask(string folderPath, string sessionPassword, IEnumerable<FileModel> models, RSAParameters privateKey, string extension)
        : base(ResourceType.Folder, folderPath)
        {
            InnerTask = new Task(() =>
            {
                var key = string.Empty;
                var keysNotCreated = new List<string>();
                var keyCreated = false;

                foreach (var model in models)
                {
                    if (ContainerHelper.ValidateContainer(model.Secured, sessionPassword))
                    {
                        if (keyCreated) continue;

                        var keyPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                            folderPath,
                            Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), 
                            extension);

                        RsaKeyWriterHelper.SerializeTextToFile(privateKey, sessionPassword, keyPath);

                        key = keyPath;
                        keyCreated = true;
                    }
                    else
                    {
                        keysNotCreated.Add(model.Secured);
                    }
                }

                Result.Value = new BulkExportKeysTaskResultModel { KeyPath = key, NotCreated = keysNotCreated };
            });
        }
    }
}
