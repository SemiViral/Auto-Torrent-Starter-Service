using System.Diagnostics;
using System.IO;

namespace AutoTorrentStarterService.Model {
    public class AutoTorrentStarter {
        public AutoTorrentStarter(string torrentDownloaderPath, string executionString) {
            _torrentDownloaderPath = torrentDownloaderPath;
            _executionString = executionString;
        }

        #region EVENTS

        public void OnTorrentAdded(object source, FileSystemEventArgs args) {
            Process.Start(_torrentDownloaderPath, $"{_executionString} {args.FullPath}");
        }

        #endregion

        #region MEMBERS

        private readonly string _executionString;
        private readonly string _torrentDownloaderPath;

        #endregion
    }
}