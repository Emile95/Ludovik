using Library.Class;
using Library.Plugins.BuildStepDefinition;
using Library.Plugins.Job;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        #region Properties and Constructor

        public List<BuildStep> BuildSteps { get; private set; }

        public StandardJob()
        {
            ClassName = "StandardJob";
            BuildSteps = new List<BuildStep>();
        }

        #endregion

        #region Job implementation

        public sealed override void PreBuild(Build build, Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {

        }

        public sealed override void Build(Build build, Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            foreach(BuildStep step in BuildSteps)
            {
                step.Apply(env, failedBuildTokenSource, loggers);
                //CheckIfBuildCanceled(taskCancelToken, loggers.GetLogger<JobBuildLogger.JobBuildLogger>());
                failedBuildTokenSource.Token.ThrowIfFailed();
            }
        }

        public sealed override void AfterBuild(Build build, Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {

        }

        #endregion

        #region IConvertable Implementation

        public sealed override string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + ClassName + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"description\":" + "\"" + Description + "\"," + "\n";

            jsonStr += depthTab + "\t" + "\"properties\":" + "[\n";

            for (int i = 1; i < Properties.Count; i++)
            {
                jsonStr += Properties[i].ToJson(true, nbTab + 2);
                jsonStr += (i < Properties.Count - 1 ? "," : "") + ",\n";
            }

            for (int i = 0; i < BuildSteps.Count; i++)
            {
                jsonStr += BuildSteps[i].ToJson(true, nbTab + 2);
                jsonStr += (i < BuildSteps.Count - 1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]" + "\n";

            jsonStr += "}";
            return jsonStr;
        }

        #endregion

        #region IConfigurable implementation

        public sealed override Config GetConfig()
        {
            return null;
        }

        public sealed override void LoadConfig(Config config)
        {
            Property property = config.GetProperties<DescriptionPropertyDefinition.DescriptionPropertyDefinition>()[0];

            Name = property.Parameters[0].Value;
            Description = property.Parameters[1].Value;

            foreach(Property prop in config.Props.Where(o => !(o.Definition is BuildStepDefinition)))
                Properties.Add(prop);
            
            config.GetProperties<BuildStepDefinition>().ForEach(o => {
                BuildSteps.Add(new BuildStep() { 
                    Definition = o.Definition as BuildStepDefinition,
                    Parameters = o.Parameters
                });
            });
        }

        #endregion
    }
}
