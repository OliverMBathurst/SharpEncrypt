using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ImportPublicRSAKeyTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ImportPublicRSAKeyTask;

        public ImportPublicRSAKeyTask(string path, (string identity, string importFilePath) importResult)
            : base(ResourceType.File, path, importResult.importFilePath)
        {
            InnerTask = new Task(() =>
            {
                IDictionary<string, RSAParameters> pubKeyList = new Dictionary<string, RSAParameters>();
                if (File.Exists(path))
                {
                    pubKeyList = RSAKeyReaderHelper.GetPublicKeys(path);

                    if (pubKeyList.ContainsKey(importResult.identity))
                    {
                        throw new DuplicateIdentityException();
                    }
                }

                pubKeyList.Add(importResult.identity, RSAKeyReaderHelper.GetParameters(importResult.importFilePath));

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, pubKeyList);
                }
            });
        }
    }
}
