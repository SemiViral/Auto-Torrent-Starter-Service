using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;

namespace AutoTorrentStarter {
    public class AutoFileSystemWatcher : IDisposable {
        public AutoFileSystemWatcher(string directoryToWatch) {
            _directoryToWatch = directoryToWatch;
            _fileSystemWatcher = new FileSystemWatcher(_directoryToWatch);
        }

        #region THREADING TASKS

        private void FileSystemWatcher_WaitForChanged(WatcherChangeTypes changeType) {
            while (Program.Running) {
                WaitForChangedResult changeResult = _fileSystemWatcher.WaitForChanged(changeType);
                OnFileChanged(new FileSystemEventArgs(changeType, Path.GetFullPath(changeResult.Name), changeResult.Name));
            }
        }

        /// <summary>
        /// Polls the watched directory for changes.
        /// </summary>
        /// <param name="pollFrequency">frequency to poll in milliseconds</param>
        private void PollingTimer_WaitForChanged(int pollFrequency) {
            while (Program.Running) {
                Thread.Sleep(pollFrequency);


            }
        }

        private void PollingTimer_OnFileChangedWrapper() {
            Stack<FileSystemEventArgs> fileSystemPoll = PollWatchDirectory();

            while (fileSystemPoll.Count > 0) {
                //TODO logic
            }
        }

        #endregion

        #region METHODS

        public void OnFileChanged(FileSystemEventArgs args) {
            lock (_fileChangedLock) {
                switch (args.ChangeType) {
                    case WatcherChangeTypes.Created:
                        OnFileSystemWatcher_Created(_fileSystemWatcher, new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetFullPath(changedResult.Name), changedResult.Name));
                        break;
                }
            }
        }

        private Stack<FileSystemEventArgs> PollWatchDirectory() {
            //TODO logic

            return new Stack<FileSystemEventArgs>();
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