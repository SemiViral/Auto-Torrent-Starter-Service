using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AutoTorrentStarterService.Model;

namespace AutoTorrentStarterService.ViewModel {
    public class AutoTorrentStarterServiceViewModel {
        public AutoTorrentStarterServiceViewModel(EventLog eventLog) {
            EventLog = eventLog;
            EventLog.EntryWritten += EventLog_EntryWritten;
        }

        #region EVENTS

        public void EventLog_EntryWritten(object sender, EntryWrittenEventArgs args) { }
        public event FileSystemEventHandler AutoFileSystemWatcher_Created;
        private void OnAutoFileSystemWatcher_Created()

        #endregion


        #region METHODS

        private static IEnumerable<string> LoadTorrents(string directory) {
            return Directory.GetFiles(directory, "*.torrent");
        }

        #endregion

        #region MEMBERS

        private AutoTorrentStarter AutoTorrentStarter { get; }
        private EventLog EventLog { get; }

        #endregion
    }
}