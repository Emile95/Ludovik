using Library.Class;
using Library.Interface;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Library.Plugins.Job
{
    public abstract class Job : ILoadable, IConfigurable, IRunnable, IRepository, IConvertable
    {
        #region Properties and Constructor

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Property> Properties { get; set; }

        public Job()
        {
            Properties = new List<Property>();
        }

        #endregion

        #region Private Methods

        private Build CreateBuild()
        {
            string buildNumberPath = "jobs\\" + Name + "\\nextBuildNumber";
            string buildNumberStr = File.ReadAllText(buildNumberPath);
            int buildNumber = Convert.ToInt32(buildNumberStr);

            Build build = new Build(buildNumber++, "#" + buildNumberStr, "");

            //Create Build Repository
            build.CreateRepository("jobs\\" + Name + "\\builds");

            //Incremente the build in the file
            File.WriteAllText(buildNumberPath, buildNumber.ToString());

            return build;
        }

        #endregion

        #region Protected Methods

        protected void CheckIfBuildCanceled(CancellationToken taskCancelToken, JobBuildLogger logger, Action action = null)
        {
            //Verify if the build was cancelled
            if (taskCancelToken.IsCancellationRequested)
            {
                logger.Log(new Log("Cancelled at " + DateTime.Now));
                taskCancelToken.ThrowIfCancellationRequested();
                action();
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void Build(Build build, Class.Environment env, CancellationToken taskCancelToken, LoggerList loggers);

        public virtual void PreBuild(Build build, Class.Environment env, CancellationToken taskCancelToken, LoggerList loggers) { }

        public virtual void AfterBuild(Build build, Class.Environment env, CancellationToken taskCancelToken, LoggerList loggers) { }

        #endregion

        #region ILoadable implementation

        public virtual void LoadFromFolder(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");

            JArray propConfigObjects = configFileObject
                .Value<JArray>("properties");

             foreach(JToken propConfigObject in propConfigObjects)
             {
                Property prop = new Property() {
                    Definition = PluginStorage.CreateObject<PropertyDefinition.PropertyDefinition>(propConfigObject.Value<string>("_class"))
                };
                foreach(JToken parameterConfigObject in propConfigObject.Value<JArray>("parameters"))
                {
                    prop.Parameters.Add(new Parameter() { 
                        Definition = PluginStorage.CreateObject<ParameterDefinition.ParameterDefinition>(parameterConfigObject.Value<string>("_class")),
                        Name = parameterConfigObject.Value<string>("name"),
                        Value = parameterConfigObject.Value<string>("value")
                    });
                }
                Properties.Add(prop);
             }

             Properties.Add(new Property() { 
                Definition = new DescriptionPropertyDefinition(),
                Parameters = new List<Parameter>() { 
                    new Parameter() { 
                        Definition = new StringParameterDefinition() ,
                        Name = "name",
                        Value = this.Name
                    },
                    new Parameter() {
                        Definition = new StringParameterDefinition() ,
                        Name = "description",
                        Value = this.Description
                    }
                }
             });
        }

        #endregion

        #region IConvertable Implementation

        public abstract string ToJson(bool beautify, int nbTab = 0);

        #endregion

        #region IConfigurable implementation

        public abstract Config GetConfig();

        public abstract void LoadConfig(Config config);

        #endregion

        #region IRunnable Implementation

        public void Run(CancellationToken taskCancelToken, LoggerList loggers)
        {
            //Create Build Object
            Build build = CreateBuild();
            //Create Job Build logger
            JobBuildLogger buildLogger = new JobBuildLogger(Name, build.Number);

            CheckIfBuildCanceled(taskCancelToken,buildLogger);

            //Start Execution

            //Create the build Environment
            Class.Environment env = new Class.Environment();

            Property descriptionProperty = Properties.Single(o => o.Definition is DescriptionPropertyDefinition);

            descriptionProperty.Definition.Apply(env, descriptionProperty.Parameters.ToArray());

            Properties
                .Where(o => !(o.Definition is DescriptionPropertyDefinition))
                .ToList()
                .ForEach(prop => prop.Definition.Apply(env, prop.Parameters.ToArray()));

            env.Properties.Add("buildNumber", build.Number.ToString());

            buildLogger.Log(new Log("Start at " + DateTime.Now + "\n"));

            PreBuild(build, env, taskCancelToken, loggers);
            Build(build, env, taskCancelToken, loggers);
            AfterBuild(build, env, taskCancelToken, loggers);

            buildLogger.Log(new Log("End at " + DateTime.Now));
        }

        #endregion

        #region IRepository Implementation

        public virtual void CreateRepository(string path)
        {
            Directory.CreateDirectory(path+"\\"+Name);

            File.WriteAllText(path + "\\" + Name + "\\nextBuildNumber", "1");

            string dirPath = path + "\\" + Name;

            string configFile = dirPath + "\\config.json";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(File.Create(configFile)))
            {
                file.WriteLine(ToJson(true));
            }

            //Create Builds Directory
            Directory.CreateDirectory(dirPath + "\\builds");
        }

        #endregion
    }
}
