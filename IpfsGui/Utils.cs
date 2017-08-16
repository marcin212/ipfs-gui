using System.Diagnostics;
using System.Windows.Forms;
using ipfs_gui.resources;

namespace IpfsGui
{
    public class Utils
    {
        public static Process CreateHiddenProcess(string file, string args)
        {
            Process process = new Process();
            process.StartInfo.Arguments = args;
            process.StartInfo.FileName = file;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            return process;
        }

        public static void ShowBalloonTip(string title, string message)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Icons.ipfs;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(800, title, message, ToolTipIcon.None);
            notifyIcon.Visible = false;
        }
    }
}