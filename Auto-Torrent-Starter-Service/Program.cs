namespace Auto_Torrent_Starter_Service {
    public static class Program {
        public static void Main(string[] args) {
            _autoTorrentStarter = new AutoTorrentStarter();
        }

        #region MEMBERS

        private static AutoTorrentStarter _autoTorrentStarter;

        #endregion
    }
}
