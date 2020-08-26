using System;
using System.IO;
using System.Threading.Tasks;
using AesLibrary;
using FileGeneratorLibrary;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.File_Tasks
{
    public sealed class ContainerizeFileTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.ContainerizeFileTask;

        public ContainerizeFileTask(string filePath, ContainerizationSettingsModel settingsModel) : base(ResourceType.File, filePath)
        {
            InnerTask = new Task(() =>
            {
                if (Path.GetExtension(filePath).Equals(settingsModel.Extension)) return;

                ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), settingsModel.Password);
                var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    DirectoryHelper.GetDirectoryPath(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    settingsModel.Extension);

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
