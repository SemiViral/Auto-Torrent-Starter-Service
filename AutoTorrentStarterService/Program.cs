using System.ServiceProcess;

namespace AutoTorrentStarterService {
    public static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main() {
            ServiceBase[] servicesToRun = {
                new View.AutoTorrentStarterService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}