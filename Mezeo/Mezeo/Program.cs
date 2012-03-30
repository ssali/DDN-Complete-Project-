﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Mezeo
{
    static class Program
    {
        static Mutex mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {//// {D19F100E-113F-4751-820C-FD5AF8D17A55}

            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = fileInfo.Name; 
            bool bCreatedNew;

            mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();
            else
            {
                BasicInfo.LoadRegistryValues();
                if (BasicInfo.SyncDirPath.Trim().Length != 0)
                {
                    string argument = BasicInfo.SyncDirPath;
                    System.Diagnostics.Process.Start(argument);
                }
                //else
                //{
                //    SwitchToCurrentInstance();
                //}
                return; 
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            frmLogin loginForm = new frmLogin();
            bool showLogin = loginForm.showLogin;
            
            if (showLogin)
            {
                Application.Run(loginForm);
            }
            else
            {
                loginForm.Login();
                Application.Run();
            }

        }
    }
}
