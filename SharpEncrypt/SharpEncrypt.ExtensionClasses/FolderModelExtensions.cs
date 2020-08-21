using System;
using System.Collections.Generic;
using System.Linq;
using SharpEncrypt.Models;

namespace SharpEncrypt.ExtensionClasses
{
    public static class FolderModelExtensions
    {
        internal static FolderModel GetSubFolder(this FolderModel masterModel, Func<FolderModel, bool> predicate)
        {
            foreach (var subFolderModel in masterModel.SubFolders)
            {
                if (predicate(subFolderModel))
                    return subFolderModel;

                var model = GetSubFolder(subFolderModel, predicate);
                if (model != null)
                    return model;
            }

            return null;
        }

        internal static FileModel GetFileModel(this FolderModel folderModel, Func<FileModel, bool> predicate)
        {
            foreach (var fileModel in folderModel.FileModels.Where(predicate))
                return fileModel;

            return folderModel.SubFolders.Select(subFolderModel => GetFileModel(subFolderModel, predicate)).FirstOrDefault(fileModel => fileModel != null);
        }

        internal static IEnumerable<FileModel> GetFileModels(this FolderModel folderModel, Func<FileModel, bool> predicate)
            => folderModel.FileModels
                .Where(predicate)
                .Concat(folderModel.SubFolders.SelectMany(x => GetFileModels(x, predicate)));
    }
}