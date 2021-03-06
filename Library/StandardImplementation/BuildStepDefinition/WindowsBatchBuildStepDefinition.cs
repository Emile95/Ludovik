﻿using Library.Class;
using Library.Plugins.BuildStepDefinition;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Library.StandardImplementation.WindowsBatchBuildStepDefinition
{
    public class WindowsBatchBuildStepDefinition : BuildStepDefinition
    {
        public WindowsBatchBuildStepDefinition()
        {
            ClassName = "WindowsBatchBuildStepDefinition";
        }

        #region PropertyDefinition Implementations

        public sealed override void Apply(Environment env, Parameter[] parameters, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            string command = parameters.Single(o => o.Name == "command").Value;

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);

            string directory = Path.Combine(System.Environment.CurrentDirectory, "jobs", env.Properties["jobName"]);

            JobBuildLogger.JobBuildLogger buildLogger = loggers.GetLogger<JobBuildLogger.JobBuildLogger>();

            buildLogger.Log(new Log("[windows-batch] : " + command));
            
            Process process = new Process();
            
            process.StartInfo.WorkingDirectory = directory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += (sender, args) => buildLogger.Log(new Log(args.Data));
            process.ErrorDataReceived += (sender, args) => buildLogger.Log(new Log(args.Data));

            foreach(KeyValuePair<string, string> prop in env.Properties)
                process.StartInfo.Environment.Add(prop.Key.ToUpper(),prop.Value);

            Node node = new Node() { 
                WorkSpace = "C:",
                IpAddress = "machine"
            };

            node.Execute(process, command);

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            process.WaitForExit();

            if (process.ExitCode != 0)
                failedBuildTokenSource.Failed();
        }

        #endregion
    }
}
