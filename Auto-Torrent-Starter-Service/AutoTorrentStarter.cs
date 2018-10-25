using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Auto_Torrent_Starter_Service {
    public class AutoTorrentStarter {
        public AutoTorrentStarter(string saveDirectory, string watchDirectory, string torrentPath) {
            _saveDirectory = @"C:\Users\semiv\OneDrive\Documents\Torrents\Temp";
            _watchDirectory = @"C:\Users\semiv\OneDrive\Documents\Torrents\Temp";
            _torrentPath = @"C:\Users\semiv\AppData\Roaming\uTorrent\uTorrent.exe";

            AutoFileSystemWatcher = new AutoFileSystemWatcher(_watchDirectory);
            AutoFileSystemWatcher.FileSystemWatcher_Created += OnAutoFileSystemWatcher_Created;

            Initialise();
        }

        #region INIT

        private void Initialise() {
            foreach (string filePath in LoadTorrents(_watchDirectory)) {
                FileInfo file = new FileInfo(filePath);

                if (file.Directory == null) {
                    continue;
                }

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

            Process.Start(_torrentPath, fullCommand);
        }

        #endregion

        #region MEMBERS

        private readonly string _saveDirectory;
        private readonly string _watchDirectory;
        private readonly string _torrentPath;

        private AutoFileSystemWatcher AutoFileSystemWatcher { get; }

        #endregion
    }
}