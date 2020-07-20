using SharpEncrypt.ExtensionClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;

namespace SharpEncrypt.Managers
{
    internal sealed class FileSystemManager
    {
        private readonly List<FileSystemWatcher> Watchers = new List<FileSystemWatcher>();

        #region Delegates and events

        public delegate void ItemDeletedEventHandler(string path);
        public delegate void ItemRenamedEventHandler(string newPath, string oldPath, bool subFolderItem);
        public delegate void ItemCreatedEventHandler(string itemPath, bool subfolderItem);
        public delegate void ExceptionOccurredEventHandler(Exception exception);

        public event ItemDeletedEventHandler ItemDeleted;
        public event ItemRenamedEventHandler ItemRenamed;
        public event ItemCreatedEventHandler ItemCreated;
        public event ExceptionOccurredEventHandler Exception;

        #endregion

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void AddPaths(IEnumerable<string> paths) => AddPathsInternal(paths);

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void AddPaths(params string[] paths) => AddPathsInternal(paths);

        #region Event handlers

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            OnItemRenamed(e.FullPath, e.OldFullPath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnItemDeleted(e.FullPath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            OnItemCreated(e.FullPath);
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            if(sender is FileSystemWatcher watcher && !watcher.Path.Exists())
            {
                OnFolderDeleted(watcher.Path);
            }
            else
            {
                OnException(e.GetException());
            }
        }

        #endregion

        #region Misc methods

        private void AddPathsInternal(IEnumerable<string> paths)
        {
            foreach (var path in paths.Distinct())
            {
                var watcher = GetWatcher(path);
                if (watcher != null)
                {
                    Watchers.Add(watcher);
                }
            }
        }

        private void OnItemDeleted(string path)
        {            
            ItemDeleted?.Invoke(path);
        }

        private void OnItemCreated(string path)
        {
            ItemCreated?.Invoke(path, IsInSubfolder(path));
        }

        private void OnItemRenamed(string newPath, string oldPath)
        {
            ItemRenamed?.Invoke(newPath, oldPath, IsInSubfolder(newPath));
        }

        private void OnException(Exception e)
        {
            Exception?.Invoke(e);
        }

        private void OnFolderDeleted(string path)
        {
            Watchers.RemoveAll(x => x.Path.Equals(path, StringComparison.Ordinal));
            ItemDeleted?.Invoke(path);
        }

        private bool IsInSubfolder(string path)
        {
            return !Watchers.Any(x => x.Path.Equals(Path.GetDirectoryName(path), StringComparison.Ordinal));
        }

        private FileSystemWatcher GetWatcher(string path)
        {
            if (path != null)
            {
                var dirPath = string.Empty;
                var isDir = false;
                if (Directory.Exists(path))
                {
                    dirPath = path;
                    isDir = true;
                }
                else if (File.Exists(path))
                {
                    var fileDir = Path.GetDirectoryName(path);
                    if (fileDir == null)
                    {
                        var pathRoot = Path.GetPathRoot(path);
                        if (!string.IsNullOrEmpty(pathRoot))
                        {
                            dirPath = pathRoot;
                        }
                    }
                    else if (fileDir.Length > 0)
                    {
                        dirPath = fileDir;
                    }
                }

                if (!string.IsNullOrEmpty(dirPath))
                {
                    var watcher = new FileSystemWatcher
                    {
                        Path = dirPath,
                        IncludeSubdirectories = true,
                        EnableRaisingEvents = true
                    };

                    if (!isDir)
                    {
                        var filePath = Path.GetFileName(path);
                        watcher.Filter = filePath.Length > 0 ? filePath : "*.*";
                    }

                    watcher.Created += Watcher_Created;
                    watcher.Error += Watcher_Error;
                    watcher.Deleted += Watcher_Deleted;
                    watcher.Renamed += Watcher_Renamed;

                    watcher.NotifyFilter = NotifyFilters.Attributes |
                        NotifyFilters.CreationTime |
                        NotifyFilters.FileName |
                        NotifyFilters.LastAccess |
                        NotifyFilters.LastWrite |
                        NotifyFilters.Size |
                        NotifyFilters.Security;

                    return watcher;
                }
            }

            return null;
        }

        #endregion
    }
}