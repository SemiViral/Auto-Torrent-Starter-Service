using System.ComponentModel;
using System.Configuration.Install;

namespace AutoTorrentStarterService {
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer {
        public ProjectInstaller() {
            InitializeComponent();
        }

        private void ATSSSericeInstaller_AfterInstall(object sender, InstallEventArgs e) { }
    }
}