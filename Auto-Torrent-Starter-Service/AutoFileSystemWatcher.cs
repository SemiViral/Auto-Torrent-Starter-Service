using System;
using System.IO;

namespace AutoTorrentStarter {
    public class AutoFileSystemWatcher : IDisposable {
        public AutoFileSystemWatcher(string directoryToWatch) {
            _directoryToWatch = directoryToWatch;
            _fileSystemWatcher = new FileSystemWatcher(_directoryToWatch);
        }

        #region METHODS

        public void WaitForChanged(WatcherChangeTypes changeType) {
            WaitForChangedResult changedResult =_fileSystemWatcher.WaitForChanged(changeType);

            switch(changedResult.ChangeType) {
                case WatcherChangeTypes.Created:
                    OnFileSystemWatcher_Created(_fileSystemWatcher, new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetFullPath(changedResult.Name), changedResult.Name));
                    break;
            }
        }

        #endregion

        #region MEMBERS

        private readonly string _directoryToWatch;
        private readonly FileSystemWatcher _fileSystemWatcher;

        #endregion

        #region EVENTS

        public event FileSystemEventHandler FileSystemWatcher_Created;

        private void OnFileSystemWatcher_Created(object sender, FileSystemEventArgs args) {
            FileSystemWatcher_Created?.Invoke(sender, args);
        }

        public void Dispose() {
            _fileSystemWatcher.Dispose();
        }

        #endregion
    }
}