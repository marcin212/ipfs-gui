using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using IpfsGui;

namespace IpfsGui
{
    class IpfsDaemon
    {
        public delegate void DaemonEvent();

        public event DaemonEvent OnDaemonStart;
        public event DaemonEvent OnDaemonStop;
        private Process process;

        public void Start()
        {
            if (!(process?.HasExited ?? true)) return;
            IpfsCommand.Init.ExecuteAndWait();
            process = IpfsCommand.Daemon.GetProcess();
            process.EnableRaisingEvents = true;
            process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data); //TODO Logger
            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data); //TODO Logger
            process.Exited += (sender, args) => { OnDaemonStop?.Invoke(); };
            process.Start();
            OnDaemonStart?.Invoke();
        }

        public void Stop()
        {
            IpfsCommand.Shutdown.ExecuteAndWait();
            process?.WaitForExit();
        }
    }
}