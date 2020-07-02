using Library.Class;
using Library.Interface;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
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

        #region Abstract Methods

        public abstract void Build(Build build, CancellationToken taskCancelToken, LoggerList loggers);

        public virtual void PreBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers) { }

        public virtual void AfterBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers) { }

        #endregion

        #region ILoadable implementation

        public virtual void LoadFromFolder(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");
        }

        #endregion

        #region IConvertable Implementation

        public abstract string ToJson(bool beautify, int nbTab = 0);

        #endregion

        #region IConfigurable implementation

        public Config GetConfig()
        {
            string configFile = File.ReadAllText("jobs\\"+Name + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Config config = new Config();
            /*config.AddParameter(
                Name,
                new StringParameterDefinition("Name","Name of this job")
            );
            config.AddParameter(
                configFileObject.Value<string>("description"),
                new StringParameterDefinition("Description", "Description of this job")
            );
            */
            return config;
        }

        public void LoadConfig(Config config)
        {
            Property descriptionProperty = config.GetProperty<DescriptionPropertyDefinition>();

            Name = descriptionProperty.Parameters[0].Value;
            Description = descriptionProperty.Parameters[1].Value;

            Property[] props = config.GetProperties().Where(o => o.Definition.GetType() != typeof(DescriptionPropertyDefinition)).ToArray();

            foreach (Property prop in props)
            {
                Properties.Add(prop);
            }
        }

        #endregion

        #region IRunnable Implementation

        public void Run(CancellationToken taskCancelToken, LoggerList loggers)
        {
            string buildNumberPath = "jobs\\" + Name + "\\nextBuildNumber";
            string buildNumberStr = File.ReadAllText(buildNumberPath);
            int buildNumber = Convert.ToInt32(buildNumberStr);

            Build build = new Build(buildNumber++, "#"+buildNumberStr,"");
            JobBuildLogger buildLogger = new JobBuildLogger(Name, build.Number);

            build.CreateRepository("jobs\\"+Name+"\\builds");

            File.WriteAllText(buildNumberPath, buildNumber.ToString());

            if(taskCancelToken.IsCancellationRequested)
            {
                buildLogger.Log(new Log("Cancelled at " + DateTime.Now));
                taskCancelToken.ThrowIfCancellationRequested();
            }

            buildLogger.Log(new Log("Start at " + DateTime.Now));

            PreBuild(build, taskCancelToken, loggers);
            Build(build, taskCancelToken, loggers);
            AfterBuild(build, taskCancelToken, loggers);

            buildLogger.Log(new Log("End at " + DateTime.Now));
        }

        #endregion

        #region IRepository Implementation

        public virtual void CreateRepository(string path)
        {
            Directory.CreateDirectory(path+"\\"+Name);
            File.WriteAllText(path + "\\" + Name + "\\nextBuildNumber", "1");
        }

        #endregion

    }
}
