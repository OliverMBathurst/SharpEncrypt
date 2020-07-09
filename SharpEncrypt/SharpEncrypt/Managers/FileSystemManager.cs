using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpEncrypt.Managers
{
    internal sealed class FileSystemManager
    {
        private readonly Dictionary<string, FileSystemWatcher> Watchers = new Dictionary<string, FileSystemWatcher>();

        #region Delegates and events

        public delegate void FileDeletedEventHandler(string filePath);
        public delegate void FolderDeletedEventHandler(string folderPath);
        public delegate void FileRenamedEventHandler(string oldPath, string newPath);
        public delegate void FolderRenamedEventHandler(string oldPath, string newPath);
        public delegate void ExceptionOccurredEventHandler(Exception exception);

        public event FileDeletedEventHandler FileDeleted;
        public event FolderDeletedEventHandler FolderDeleted;
        public event FileRenamedEventHandler FileRenamed;
        public event FolderRenamedEventHandler FolderRenamed;
        public event ExceptionOccurredEventHandler Exception;

        #endregion

        public void AddPaths(IEnumerable<string> paths)
        {
            foreach(var path in paths.Distinct())
            {
                if (path != null)
                {
                    var dirPath = string.Empty;
                    if (Directory.Exists(path))
                    {
                        dirPath = path;
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

                        watcher.NotifyFilter = NotifyFilters.DirectoryName;
                        watcher.Changed += FolderChanged;
                        watcher.Error += Error;
                        Watchers.Add(dirPath, watcher);
                    }
                }
            }
        }

        private void Error(object sender, ErrorEventArgs e)
        {
            Exception?.Invoke(e.GetException());            
        }

        #region Event handlers
        private void FolderChanged(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Deleted:
                    OnFolderDeleted(e.FullPath);
                    break;
                case WatcherChangeTypes.Renamed:
                    OnFolderRenamed(e.FullPath);
                    break;
            }
        }
        #endregion

        #region Misc methods

        private void OnFolderDeleted(string folderPath)
        {
            Watchers.Remove(folderPath);
            FolderDeleted?.Invoke(folderPath);
        }

        private void OnFolderRenamed(string folderPath)
        {
            //get original name of file
            //get changed name
            //change dictionary key to reflect new path
            //update ui with the new name
            //create overloaded constructor in WriteSecuredFileListTask
            //This one will set the inner task to both add entries and remove others, before writing the collection to securedFileList
            //Create an object of that task with the old name (to remove) and the new one (to add) as arguments, add it to the taskmanager

            //get the key value pair to get the key (original file name)
            var kvp = Watchers.First(x => x.Value.Path.Equals(folderPath, StringComparison.Ordinal));
            //invoke with orig. file name and new file name
            FileRenamed?.Invoke(kvp.Key, folderPath);
            //remove old watcher
            Watchers.Remove(kvp.Key);

            //create and add new watcher for the renamed file
            using (var watcher = new FileSystemWatcher { Path = folderPath })
            {
                watcher.Changed += FolderChanged;
                Watchers.Add(folderPath, watcher);
            }
        }

        #endregion
    }
}