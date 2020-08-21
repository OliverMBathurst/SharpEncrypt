using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AesLibrary;
using FileGeneratorLibrary;
using SharpEncrypt.Models;

namespace SharpEncrypt.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetDirectoryPath(string dir)
        {
            if (dir == null)
                throw new ArgumentNullException(nameof(dir));

            var dirInfo = Path.GetDirectoryName(dir);
            if (dirInfo == null)
                return Directory.GetDirectoryRoot(dir);
            
            return dirInfo.Length > 0 ? dirInfo : null;
        }

        public static IEnumerable<FolderModel> EnumerateAndSecureSubFolders(string folderPath, ContainerizationSettingsModel settingsModel) =>
            Directory.GetDirectories(folderPath).Select(x
                => new FolderModel
                {
                    Uri = x,
                    FileModels = EnumerateAndSecureFiles(x, settingsModel).ToList(),
                    SubFolders = EnumerateAndSecureSubFolders(x, settingsModel).ToList()
                });

        public static IEnumerable<FileModel> EnumerateAndSecureFiles(string folderPath, ContainerizationSettingsModel settingsModel)
        {
            var fileModels = new List<FileModel>();

            foreach (var filePath in Directory.GetFiles(folderPath).Where(x => !Path.GetExtension(x).Equals(settingsModel.Extension)))
            {
                ContainerHelper.ContainerizeFile(filePath, AesHelper.GetNewAesKey(), settingsModel.Password);
                var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    GetDirectoryPath(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    settingsModel.Extension);

                File.Move(filePath, newPath);

                fileModels.Add(new FileModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = newPath
                });
            }

            return fileModels;
        }

        public static void DecontainerizeDirectoryFiles(FolderModel masterModel, string folderPath, ContainerizationSettingsModel settingsModel, bool includeSubFolders)
        {
            foreach (var filePath in Directory.GetFiles(folderPath).Where(x => Path.GetExtension(x).Equals(settingsModel.Extension)))
            {
                ContainerHelper.DecontainerizeFile(filePath, settingsModel.Password);

                var fileModel = folderPath.Equals(masterModel.Uri)
                    ? masterModel.FileModels.FirstOrDefault(x => x.Secured.Equals(filePath))
                    : GetFileModel(masterModel, file => file.Secured.Equals(filePath));

                var newPath = FileGeneratorHelper.GetValidFileNameForDirectory(
                    GetDirectoryPath(filePath),
                    Path.GetFileNameWithoutExtension(filePath),
                    fileModel != null ? Path.GetExtension(fileModel.File) : string.Empty);

                File.Move(filePath, newPath);
            }

            if (!includeSubFolders) return;

            foreach (var subFolderPath in Directory.GetDirectories(folderPath))
                DecontainerizeDirectoryFiles(masterModel, subFolderPath, settingsModel, true);
        }

        public static FolderModel GetSubFolderModel(FolderModel masterModel, Func<FolderModel, bool> predicate)
        {
            foreach (var subFolderModel in masterModel.SubFolders.Where(predicate))
                return subFolderModel;

            return masterModel.SubFolders.Select(subFolder => GetSubFolderModel(subFolder, predicate))
                .FirstOrDefault(model => model != null);
        }

        public static FileModel GetFileModel(FolderModel masterModel, Func<FileModel, bool> predicate)
        {
            foreach (var fileModel in masterModel.FileModels.Where(predicate))
                return fileModel;

            return masterModel.SubFolders.Select(subFolder => GetFileModel(subFolder, predicate))
                .FirstOrDefault(model => model != null);
        }
    }
}