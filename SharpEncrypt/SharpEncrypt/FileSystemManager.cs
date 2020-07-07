using System.Collections.Generic;
using System.IO;

namespace SharpEncrypt
{
    internal sealed class FileSystemManager
    {
        private readonly List<FileSystemWatcher> Watchers = new List<FileSystemWatcher>();

        public delegate void FileDeletedEventHandler(string filePath);
        public event FileDeletedEventHandler FileDeleted;

        public void AddFiles(IEnumerable<string> filePaths)
        {

        }

        public void AddFolders(IEnumerable<string> folderPaths)
        {

        }
    }
}
