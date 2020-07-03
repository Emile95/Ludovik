using System.Diagnostics;

namespace Library.Class
{
    public class Node
    {
        public string IpAddress { get; set; }
        public string WorkSpace { get; set; }

        public void Execute(Process process, string command)
        {
            process.StartInfo.FileName = "psexec.exe";
            process.StartInfo.Arguments = "\\"+IpAddress + " " + command;

            process.Start();

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            process.WaitForExit();
        }
    }
}
