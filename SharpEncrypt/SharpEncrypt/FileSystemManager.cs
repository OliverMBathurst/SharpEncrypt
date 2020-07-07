using System.Collections.Generic;
using System.IO;

namespace SharpEncrypt
{
    internal sealed class FileSystemManager
    {
        private readonly List<FileSystemWatcher> Watchers = new List<FileSystemWatcher>();

        public delegate void FileDeletedEvent(string filePath);
        public event FileDeletedEvent FileDeleted;

        public void AddFiles(IEnumerable<string> filePaths)
        {

        }

        public void AddFolders(IEnumerable<string> folderPaths)
        {

        }
    }
}
