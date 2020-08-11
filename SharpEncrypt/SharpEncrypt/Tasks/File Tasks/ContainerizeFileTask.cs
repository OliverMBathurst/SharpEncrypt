using System;
using System.IO;
using System.Threading.Tasks;
using AesLibrary;
using FileGeneratorLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    internal sealed class ContainerizeFileTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ContainerizeFileTask;

        public ContainerizeFileTask(string filePath, ContainerizationSettings settings) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (Path.GetExtension(filePath).Equals(settings.Extension)) return;

                ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), settings.Password);
                var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    DirectoryHelper.GetDirectoryPath(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    settings.Extension);

                File.Move(filePath, newPath);

                Result.Value = new FileModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = newPath
                };
            });
        }
    }
}
