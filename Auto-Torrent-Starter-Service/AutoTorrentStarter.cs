using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutoTorrentStarter {
    public class AutoTorrentStarter {
        public AutoTorrentStarter(string saveDirectory, string watchDirectory, string torrenterPath) {
            _saveDirectory = saveDirectory;
            _watchDirectory = watchDirectory;
            _torrenterPath = torrenterPath;

            AutoFileSystemWatcher = new AutoFileSystemWatcher(_watchDirectory, 16000);
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

        private void OnAutoFileSystemWatcher_Created(object sender, FileSystemEventArgs args) {
            string fullCommand = $"-m \"{args.FullPath}\"";

            try {
                Process.Start(_torrenterPath, fullCommand);
            } catch (Exception e) {
                using (FileStream logStream = File.OpenWrite("~/log")) {
                    logStream.Write(Encoding.ASCII.GetBytes($"{e.Source} => {e.Message}"));
                    logStream.Flush();
                }
            }
        }

        #endregion

        #region MEMBERS

        private readonly string _saveDirectory;
        private readonly string _watchDirectory;
        private readonly string _torrenterPath;

        public AutoFileSystemWatcher AutoFileSystemWatcher { get; }

        #endregion
    }
}