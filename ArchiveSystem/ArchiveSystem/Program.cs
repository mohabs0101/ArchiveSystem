﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ArchiveSystem
{
    internal static class Program
    {
        public static Config config = null;
        private static ScanDialog main_instance = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Log("Application started");
            config = Config.Deserialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main_instance = new ScanDialog();


            Application.Run(new Login());

            // main_instance = new Main();
            //Application.Run(main_instance);
            Logger.Log("Application closed");

        }
        public static ScanDialog GetInstance()
        {
            return main_instance;
        }

        public static void ShowError(Exception e)
        {
            Logger.Log("[Error] " + e.Message);
            if (Program.config.ShowErrors)
                MessageBox.Show(e.Message, "Error");
        }

        public static void ShowError(String s)
        {
            Logger.Log("[Error] " + s);
            if (Program.config.ShowErrors)
                MessageBox.Show(s, "Error");
        }
    }
}
