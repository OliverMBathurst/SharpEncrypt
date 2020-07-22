using System;
using System.IO;
using System.Threading.Tasks;
using AesLibrary;
using FileGeneratorLibrary;
using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ContainerizeFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ContainerizeFileTask;

        public ContainerizeFileTask(string filePath, string password, string ext) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), password);
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
                    Algorithm = CipherType.Aes
                };
            });
        }
    }
}
