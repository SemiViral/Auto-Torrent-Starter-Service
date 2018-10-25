namespace AutoTorrentStarter {
    public static class Program {
        public static void Main(string[] args) {
#if DEBUG
            args = new string[3];
            args[0] = @"C:\Users\semiv\OneDrive\Documents\Torrents\Temp";
            args[1] = @"C:\Users\semiv\OneDrive\Documents\Torrents\Temp";
            args[2] = @"C:\Users\semiv\AppData\Roaming\uTorrent\uTorrent.exe";
#endif

            _autoTorrentStarter = new AutoTorrentStarter(args[0], args[1], args[2]);

            Running = true;

            while (Running) {
                _autoTorrentStarter.AutoFileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes.Created);
            }


        }

        #region MEMBERS

        private static AutoTorrentStarter _autoTorrentStarter;

        public static bool Running { get; private set; }

        #endregion
    }
}
