using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;
using OtpLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    internal sealed class OtpSavePasswordsTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.OtpSavePasswordsTask;

        public OtpSavePasswordsTask(string storePath, string keyPath, IReadOnlyCollection<PasswordModel> models) : base(ResourceType.File, storePath, keyPath)
        {
            InnerTask = new Task(() =>
            {
                if (!File.Exists(storePath))
                    throw new FileNotFoundException(nameof(storePath));
                if (!File.Exists(keyPath))
                   throw new FileNotFoundException(nameof(keyPath));

                OtpHelper.Transform(storePath, keyPath);

                using (var fs = new FileStream(storePath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fs, models);
                }

                var storeLength = new FileInfo(storePath).Length;
                var keyLength = new FileInfo(keyPath).Length;
                if (storeLength > keyLength)
                {
                    var increase = storeLength - keyLength;
                    using (var crypto = new RNGCryptoServiceProvider()) 
                    {
                        using (var fs = new FileStream(keyPath, FileMode.Append))
                        {
                            var buffer = new byte[1024];
                            while(increase > 0)
                            {
                                if (buffer.Length > increase)
                                    buffer = new byte[increase];

                                crypto.GetNonZeroBytes(buffer);

                                fs.Write(buffer, 0, buffer.Length);
                                increase -= buffer.Length;
                            }
                        }
                    }
                }

                OtpHelper.Transform(storePath, keyPath);
            });
        }
    }
}
