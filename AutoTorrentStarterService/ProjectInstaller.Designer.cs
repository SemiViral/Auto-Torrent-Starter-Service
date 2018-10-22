namespace AutoTorrentStarterService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ATSSServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ATSSSericeInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ATSSServiceProcessInstaller
            // 
            this.ATSSServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.ATSSServiceProcessInstaller.Password = null;
            this.ATSSServiceProcessInstaller.Username = null;
            // 
            // ATSSSericeInstaller
            // 
            this.ATSSSericeInstaller.Description = "Windows service that monitors a directory and automatically begins the download o" +
    "f torrents added to it.";
            this.ATSSSericeInstaller.DisplayName = "Auto Torrent Starter";
            this.ATSSSericeInstaller.ServiceName = "Auto Torrent Starter";
            this.ATSSSericeInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ATSSSericeInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ATSSServiceProcessInstaller,
            this.ATSSSericeInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ATSSServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ATSSSericeInstaller;
    }
}