using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace BHSScannerApp
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();


        }

        private void Installer1_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                Settings frmSetting = new Settings();
                frmSetting.ShowDialog();
                frmSetting.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

    }
}
