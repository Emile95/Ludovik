﻿using Library.Class;
using Library.Class.Node;
using Library.Plugins.BuildStepDefinition;
using System.Collections.Generic;
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

        public sealed override void Apply(Environment env, Parameter[] parameters, Node node, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            string command = parameters.Single(o => o.Name == "command").Value;

            JobBuildLogger.JobBuildLogger buildLogger = loggers.GetLogger<JobBuildLogger.JobBuildLogger>();

            buildLogger.Log(new Log("[windows-batch] : " + command));

            Encodable.ProcessRunInfo info = new Encodable.ProcessRunInfo()
            {
                fileName = "cmd.exe",
                args = "/c" + command,
                workingDirectory = env.Properties["jobName"]
            };

            foreach (KeyValuePair<string, string> prop in env.Properties)
                info.vars += prop.Key.ToUpper() + ":" + prop.Value + ",";

            info.vars = info.vars.Substring(0, info.vars.Length - 1);

            node.RunProcess(info);
        }

        #endregion
    }
}
