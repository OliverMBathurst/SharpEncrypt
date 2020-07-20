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
                var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    Path.GetDirectoryName(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    ext);

                File.Move(filePath, newPath);


                Result.Value = new FileDataGridItemModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = newPath,
                    Algorithm = CipherType.AES
                };
            });
        }
    }
}
