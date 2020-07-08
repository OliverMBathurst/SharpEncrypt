using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpEncrypt
{
    internal sealed class FileSystemManager
    {
        private readonly Dictionary<string, FileSystemWatcher> Watchers = new Dictionary<string, FileSystemWatcher>();

        #region Delegates and events

        public delegate void FileDeletedEventHandler(string filePath);
        public delegate void FolderDeletedEventHandler(string folderPath);
        public delegate void FileRenamedEventHandler(string oldPath, string newPath);
        public delegate void FolderRenamedEventHandler(string oldPath, string newPath);

        public event FileDeletedEventHandler FileDeleted;
        public event FolderDeletedEventHandler FolderDeleted;
        public event FileRenamedEventHandler FileRenamed;
        public event FolderRenamedEventHandler FolderRenamed;

        #endregion

        public void AddFiles(IEnumerable<string> filePaths)
        {
            foreach(var filePath in filePaths.Distinct())
            {
                using (var watcher = new FileSystemWatcher { Path = filePath })
                {
                    watcher.Changed += FileChanged;
                    Watchers.Add(filePath, watcher);
                }
            }
        }

        public void AddFolders(IEnumerable<string> folderPaths)
        {
            foreach(var folderPath in folderPaths.Distinct())
            {
                using (var watcher = new FileSystemWatcher { Path = folderPath, IncludeSubdirectories = true })
                {
                    watcher.Changed += FolderChanged;
                    Watchers.Add(folderPath, watcher);
                }
            }
        }

        #region Event handlers
        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Deleted:
                    OnFileDeleted(e.FullPath);
                    break;
                case WatcherChangeTypes.Renamed:
                    OnFileRenamed(e.FullPath);
                    break;
            }
        }

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
        private void OnFileDeleted(string filePath)
        {
            Watchers.Remove(filePath);
            FileDeleted?.Invoke(filePath);
        }

        private void OnFolderDeleted(string folderPath)
        {
            Watchers.Remove(folderPath);
            FolderDeleted?.Invoke(folderPath);
        }

        private void OnFileRenamed(string filePath)
        {
            //get original name of file
            //get changed name
            //change dictionary key to reflect new path
            //update ui with the new name
            //create overloaded constructor in WriteSecuredFileListTask
            //This one will set the inner task to both add entries and remove others, before writing the collection to securedFileList
            //Create an object of that task with the old name (to remove) and the new one (to add) as arguments, add it to the taskmanager
            
            //get the key value pair to get the key (original file name)
            var kvp = Watchers.First(x => x.Value.Path.Equals(filePath, StringComparison.Ordinal));
            //invoke with orig. file name and new file name
            FileRenamed?.Invoke(kvp.Key, filePath);
            //remove old watcher
            Watchers.Remove(kvp.Key);

            //create and add new watcher for the renamed file
            using (var watcher = new FileSystemWatcher { Path = filePath })
            {
                watcher.Changed += FileChanged;
                Watchers.Add(filePath, watcher);
            }
        }

        private void OnFolderRenamed(string folderPath)
        {
            //get original name of folder
            //get changed name
            //change dictionary key to reflect new path
            //update ui with the new name
            //create overloaded constructor in WriteSecuredFoldersListTask
            //This one will set the inner task to both add entries and remove others, before writing the collection to securedFileList
            //Create an object of that task with the old name (to remove) and the new one (to add) as arguments, add it to the taskmanager
        }

        #endregion
    }
}
