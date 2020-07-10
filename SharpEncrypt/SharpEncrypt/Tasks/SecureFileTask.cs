using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Models;
using SharpEncrypt.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class SecureFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.SecureFileTask;

        public SecureFileTask(string filePath) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                Result.Value = new FileDataGridItemModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = filePath,
                    Algorithm = CipherType.AES
                };
                //change this later
            });
        }
    }
}
