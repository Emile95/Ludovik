using Library.Class;
using Library.Plugins.BuildStepDefinition;
using Library.Plugins.Job;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
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
                CheckIfBuildCanceled(taskCancelToken, build, loggers.GetLogger<JobBuildLogger.JobBuildLogger>());
                failedBuildTokenSource.Token.ThrowIfFailed();
            }
        }

        public sealed override void AfterBuild(Build build, Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {

        }

        #endregion

        #region ILoadable implementation

        public sealed override void LoadFromFolder(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");

            JArray propConfigObjects = configFileObject
                .Value<JArray>("properties");

            foreach (JToken propConfigObject in propConfigObjects)
            {
                Property prop = new Property()
                {
                    Definition = PluginStorage.CreateObject<PropertyDefinition>(propConfigObject.Value<string>("_class"))
                };
                foreach (JToken parameterConfigObject in propConfigObject.Value<JArray>("parameters"))
                {
                    prop.Parameters.Add(new Parameter()
                    {
                        Definition = PluginStorage.CreateObject<ParameterDefinition>(parameterConfigObject.Value<string>("_class")),
                        Name = parameterConfigObject.Value<string>("name"),
                        Value = parameterConfigObject.Value<string>("value")
                    });
                }

                Properties.Add(prop);
            }

            JArray buildStepConfigObjects = configFileObject
                .Value<JArray>("buildSteps");

            foreach (JToken buildStepConfigObject in buildStepConfigObjects)
            {
                BuildStep buildStep = new BuildStep()
                {
                    Definition = PluginStorage.CreateObject<BuildStepDefinition>(buildStepConfigObject.Value<string>("_class"))
                };
                foreach (JToken parameterConfigObject in buildStepConfigObject.Value<JArray>("parameters"))
                {
                    buildStep.Parameters.Add(new Parameter()
                    {
                        Definition = PluginStorage.CreateObject<ParameterDefinition>(parameterConfigObject.Value<string>("_class")),
                        Name = parameterConfigObject.Value<string>("name"),
                        Value = parameterConfigObject.Value<string>("value")
                    });
                }

                BuildSteps.Add(buildStep);
            }

            Properties.Add(new Property()
            {
                Definition = new DescriptionPropertyDefinition.DescriptionPropertyDefinition(),
                Parameters = new List<Parameter>() {
                    new Parameter() {
                        Definition = new StringParameterDefinition.StringParameterDefinition() ,
                        Name = "name",
                        Value = this.Name
                    },
                    new Parameter() {
                        Definition = new StringParameterDefinition.StringParameterDefinition() ,
                        Name = "description",
                        Value = this.Description
                    }
                }
            });
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

            jsonStr += depthTab + "\t]," + "\n";

            jsonStr += depthTab + "\t" + "\"buildSteps\":" + "[\n";

            for (int i = 0; i < BuildSteps.Count; i++)
            {
                jsonStr += BuildSteps[i].ToJson(true, nbTab + 2);
                jsonStr += (i < BuildSteps.Count - 1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]," + "\n";

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
