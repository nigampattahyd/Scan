using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwainDotNet;
using System.IO;

namespace BHSScannerApp
{
    public partial class SettingsInstaller : Form
    {
        Twain _twain;
        public SettingsInstaller()
        {
            
            InitializeComponent();
        }

        private void selectSource_Click(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string tempFilename = Application.StartupPath + "\\Settings.ini";
            StreamWriter SW = new StreamWriter(tempFilename);
            SW.WriteLine("useadp=" + useAdfCheckBox.Checked.ToString());
            SW.WriteLine("bw=" + blackAndWhiteCheckBox.Checked.ToString());
            SW.WriteLine("autoborder=" + autoDetectBorderCheckBox.Checked.ToString());
            SW.WriteLine("useui=" + useUICheckBox.Checked.ToString());
            SW.WriteLine("useduplex=" + useDuplexCheckBox.Checked.ToString());
            SW.WriteLine("grabarea=" + checkBoxArea.Checked.ToString());
            SW.WriteLine("autorotate=" + autoRotateCheckBox.Checked.ToString());
            SW.WriteLine("showprogress=" + showProgressIndicatorUICheckBox.Checked.ToString());
            SW.Close();
            SW.Dispose();

            this.Close();
        }
    }
}
