using System;
using System.Diagnostics;
using System.IO;
using ipfs_gui.resources;

namespace IpfsGui
{
    public interface ICommand
    {
        string ExecuteAndWait();
        Process Execute();
        string ExecuteAndWait(string args);
        Process Execute(string args);
        Process GetProcess();
        Process GetProcess(string args);
    }

    public class BasicIpfsCommand : ICommand
    {
        private string CommandName { get; }

        public BasicIpfsCommand(string commandName)
        {
            CommandName = commandName;
        }

        public string ExecuteAndWait()
        {
            return ExecuteAndWait(String.Empty);
        }

        public Process Execute()
        {
            return Execute(String.Empty);
        }

        public string ExecuteAndWait(string args)
        {
            Process process = Execute(args);
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        public Process Execute(string args)
        {
            Process process = GetProcess(args);
            process.Start();
            return process;
        }

        public Process GetProcess()
        {
            return GetProcess(String.Empty);
        }

        public Process GetProcess(string args)
        {
            string path = Directory.GetCurrentDirectory() +"\\"+ UserConfig.Default.IpfsPath;
            return Utils.CreateHiddenProcess(UserConfig.Default.IpfsPath, CommandName + " " + args);
        }
    }

    public static class IpfsCommand
    {
          public static ICommand Daemon => new BasicIpfsCommand("daemon");
          public static ICommand Init => new BasicIpfsCommand("init");
          public static ICommand Shutdown => new BasicIpfsCommand("shutdown");
          public static ICommand Add => new BasicIpfsCommand("add");
    }
}