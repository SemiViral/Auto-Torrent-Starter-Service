using System.Diagnostics;
using System.ServiceProcess;
using AutoTorrentStarterService.ViewModel;

namespace AutoTorrentStarterService.View {
    public partial class AutoTorrentStarterService : ServiceBase {
        private AutoTorrentStarterServiceViewModel _atssViewModel;

        public AutoTorrentStarterService() {
            InitializeComponent();

            _atssViewModel = new AutoTorrentStarterServiceViewModel(EventLog);
        }

        protected override void OnStart(string[] args) { }

        protected override void OnStop() { }

        private void EventLog_EntryWritten(object sender, EntryWrittenEventArgs e) { }
    }
}