using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using ipfs_gui.resources;

namespace IpfsGui
{
    class SystemTray
    {
        NotifyIcon notifyIcon = new NotifyIcon();

        public NotifyIcon NotifyIcon => notifyIcon;

        public void Initialize(IpfsGuiApplication app)
        {
            MenuItem start = new MenuItem(Messages.START_DAEMON, (s, e) => app.Daemon.Start());
            MenuItem stop = new MenuItem(Messages.STOP_DAEMON, (s, e) => app.Daemon.Stop());
            stop.Enabled = false;
            app.Daemon.OnDaemonStart += () =>
            {
                start.Enabled = false;
                stop.Enabled = true;
            };
            app.Daemon.OnDaemonStop += () =>
            {
                start.Enabled = true;
                stop.Enabled = false;
            };

            notifyIcon.Icon = Icons.ipfs;
            notifyIcon.Text = Messages.APP_NAME;
            notifyIcon.ContextMenu = new ContextMenu();
            MenuItem m = new MenuItem(Messages.AUTOSTART_DAEMON);
            m.Checked = UserConfig.Default.Autostart;
            m.Click += (sender, args) =>
            {
                m.Checked = UserConfig.Default.Autostart = !m.Checked;
                UserConfig.Default.Save();
            };
            notifyIcon.ContextMenu.MenuItems.Add(m);
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem(Messages.IPFS_WEB_GUI,
                (s, e) => { System.Diagnostics.Process.Start(UserConfig.Default.WebGuiAddress); }));
            notifyIcon.ContextMenu.MenuItems.Add("-");
            notifyIcon.ContextMenu.MenuItems.Add(start);
            notifyIcon.ContextMenu.MenuItems.Add(stop);
            notifyIcon.ContextMenu.MenuItems.Add("-");
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem(Messages.EXIT, (s, e) =>
            {
                app.Daemon.Stop();
                notifyIcon.Visible = false;
                Application.Exit();
            }));
            notifyIcon.Visible = true;
        }
    }
}