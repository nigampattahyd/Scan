using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScanAppInvoker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process.Start("rundll32.exe", "dfshim.dll,ShOpenVerbApplication " + @"D:\Client\BHSSVN\DrivingForce\TheDrivingForce\trunk\publish\install\Application Files\BHSScannerApp_1_0_0_5\BHSScannerApp.application");
          
            //Application.Run(new Form1());
        }
    }
}
