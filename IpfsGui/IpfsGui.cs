using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using ipfs_gui.resources;
using IpfsGui;

namespace IpfsGui
{
    class IpfsGuiApplication : ApplicationContext
    {
        public IpfsDaemon Daemon { get; }
        public SystemTray SystemTray { get; }

        private static IpfsGuiApplication _myApplicationInstance;

        public static IpfsGuiApplication MyApplicationInstance => _myApplicationInstance ??
                                                                  (_myApplicationInstance = new IpfsGuiApplication());

        public IpfsGuiApplication()
        {
            Daemon = new IpfsDaemon();
            SystemTray = new SystemTray();
            Initialize();
        }

        public void Initialize()
        {
            SystemTray.Initialize(this);
            if (UserConfig.Default.Autostart)
            {
                Daemon.Start();
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (File.Exists(args[0]) || Directory.Exists(args[0]))
                {
                    IpfsAddFileApplication.Instance.AddFile(args[0]);
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(MyApplicationInstance);
            }
        }
    }

    class IpfsAddFileApplication
    {
        private static IpfsAddFileApplication _instance;
        public static IpfsAddFileApplication Instance => _instance ?? (_instance = new IpfsAddFileApplication());

        public void AddFile(string file)
        {
            string result = IpfsCommand.Add.ExecuteAndWait("-q -w -r " + file);
            string hash = result.TrimEnd('\n').Split('\n').Last();
            Clipboard.SetText(hash);
            Utils.ShowBalloonTip(Messages.FILE_ADDED, hash);
        }
    }
}