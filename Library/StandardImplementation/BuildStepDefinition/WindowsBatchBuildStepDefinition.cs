using Library.Class;
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

            /*
            node.RunProcess(
                "cmd.exe",
                "/c" + command,
                env.Properties,
                env.Properties["jobName"]
            );*/
        }

        #endregion
    }
}
