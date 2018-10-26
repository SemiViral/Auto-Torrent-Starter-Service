using System;
using System.IO;

namespace AutoTorrentStarter {
    public class AutoFileSystemWatcher : IDisposable {
        public AutoFileSystemWatcher(string directoryToWatch, int bufferSize) {
            _fileChangedLock = new object();
            _directoryToWatch = directoryToWatch;
            _fileSystemWatcher = new FileSystemWatcher(_directoryToWatch) {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName,
                IncludeSubdirectories = true
            };

            if (bufferSize > 64000) {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, $"Parameter '{nameof(bufferSize)}' cannot be greater than 64000 bytes.");
            }

            _fileSystemWatcher.InternalBufferSize = bufferSize;
        }

        #region THREADING TASKS

        public void WaitForChanged(WatcherChangeTypes changeType) {
            while (Program.Running) {
                WaitForChangedResult changeResult = _fileSystemWatcher.WaitForChanged(changeType);
                OnFileChanged(new FileSystemEventArgs(changeType, Path.GetFullPath(changeResult.Name), changeResult.Name));
            }
        }

        #endregion

        #region METHODS

        public void OnFileChanged(FileSystemEventArgs args) {
            lock (_fileChangedLock) {
                switch (args.ChangeType) {
                    case WatcherChangeTypes.Created:
                        OnFileSystemWatcher_Created(_fileSystemWatcher, args);
                        break;
                }
            }
        }

        #endregion

        #region MEMBERS

        private static object _fileChangedLock;

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