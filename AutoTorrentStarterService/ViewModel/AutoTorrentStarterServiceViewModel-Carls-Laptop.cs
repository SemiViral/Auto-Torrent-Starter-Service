using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AutoTorrentStarterService.Model;

namespace AutoTorrentStarterService.ViewModel {
    public class AutoTorrentStarterServiceViewModel {
        public AutoTorrentStarterServiceViewModel(EventLog eventLog) {
            _saveDirectory = @"C:\Users\semiv\Documents\";
            _directoryToWatch = @"C:\Users\semiv\OneDrive\Documents\Torrents\Temp";
            _torrentDownaloderPath = @"C:\Users\semiv\AppData\Roaming\uTorrent\uTorrent.exe";

            EventLog = eventLog;
            EventLog.EntryWritten += EventLog_EntryWritten;

            AutoFileSystemWatcher = new AutoFileSystemWatcher(_directoryToWatch);
            AutoFileSystemWatcher.FileSystemWatcher_Created += OnAutoFileSystemWatcher_Created;

            Initialise();
        }

        #region INIT

        private void Initialise() {
            foreach (string filePath in LoadTorrents(_directoryToWatch)) {
                FileInfo file = new FileInfo(filePath);

                if (file.Directory == null) continue;

                OnAutoFileSystemWatcher_Created(this, new FileSystemEventArgs(WatcherChangeTypes.Created, file.Directory.FullName, file.Name));
            }
        }

        #endregion

        #region METHODS

        private static IEnumerable<string> LoadTorrents(string directory) {
            return Directory.GetFiles(directory, "*.torrent");
        }

        #endregion

        #region EVENTS

        public void EventLog_EntryWritten(object sender, EntryWrittenEventArgs args) { }

        private void OnAutoFileSystemWatcher_Created(object sender, FileSystemEventArgs args) {
            string fullCommand = $"/DIRECTORY \"{_saveDirectory}\" \"{args.FullPath}\"";

            Process.Start(_torrentDownaloderPath, fullCommand);
        }

        #endregion

        #region MEMBERS

        private readonly string _saveDirectory;
        private readonly string _directoryToWatch;
        private readonly string _torrentDownaloderPath;

        private AutoFileSystemWatcher AutoFileSystemWatcher { get; }
        private EventLog EventLog { get; }

        #endregion
    }
}