using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Rsa_Tasks
{
    internal sealed class ImportPublicRsaKeyTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.ImportPublicRsaKeyTask;

        public ImportPublicRsaKeyTask(string path, (string identity, string importFilePath) importResult)
            : base(ResourceType.File, path, importResult.importFilePath)
        {
            InnerTask = new Task(() =>
            {
                IDictionary<string, RSAParameters> pubKeyList = new Dictionary<string, RSAParameters>();
                if (File.Exists(path))
                {
                    pubKeyList = RsaKeyReaderHelper.GetPublicKeys(path);

                    if (pubKeyList.ContainsKey(importResult.identity))
                    {
                        throw new DuplicateIdentityException();
                    }
                }

                pubKeyList.Add(importResult.identity, RsaKeyReaderHelper.GetParameters(importResult.importFilePath));

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, pubKeyList);
                }
            });
        }
    }
}
