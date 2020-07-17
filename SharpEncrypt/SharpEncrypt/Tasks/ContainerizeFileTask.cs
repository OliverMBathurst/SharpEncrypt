using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Models;
using SharpEncrypt.Enums;
using System;
using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.Helpers;
using AESLibrary;
using FileGeneratorLibrary;

namespace SharpEncrypt.Tasks
{
    internal sealed class ContainerizeFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ContainerizeFileTask;

        public override bool IsSpecial => false;

        public ContainerizeFileTask(string filePath, string password, string ext) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.ContainerizeFile(filePath, AESHelper.GetNewAESKey(), password);
                var path = FileGeneratorHelper.GetValidFileNameForDirectory(filePath, ext);

                File.Move(filePath, path);


                Result.Value = new FileDataGridItemModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = path,
                    Algorithm = CipherType.AES
                };
            });
        }
    }
}
