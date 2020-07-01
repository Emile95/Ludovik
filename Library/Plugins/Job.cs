using Library.Class;
using Library.Interface;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Library.Plugins.Job
{
    public abstract class Job : ILoadable, IConfigurable, IRunnable, IRepository, IConvertable
    {
        #region Properties

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Tuple<PropertyDefinition,string[]>> Properties { get; set; }

        public Job()
        {
            Properties = new List<Tuple<PropertyDefinition, string[]>>();
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
            foreach(KeyValuePair<PropertyDefinition, string[]> prop in config.Props)
            {
                Properties.Add(new Tuple<PropertyDefinition, string[]>(prop.Key,prop.Value));
            }

            Name = Properties[0].Item2[0];
            Description = Properties[0].Item2[1];
        }

        #endregion

        #region IRunnable Implementation

        public void Run(CancellationToken taskCancelToken, LoggerList loggers)
        {
            for(double x = 0; x < 500000000; x+=0.5)
            {
                taskCancelToken.ThrowIfCancellationRequested();
            }

            Build build = new Build(1,"#1","");
            build.CreateRepository("jobs\\"+Name+"\\builds");

            loggers.AddLogger(new JobBuildLogger(Name,build.Number));

            PreBuild(build, taskCancelToken, loggers);
            Build(build, taskCancelToken, loggers);
            AfterBuild(build, taskCancelToken, loggers);
        }

        #endregion

        #region IRepository Implementation

        public virtual void CreateRepository(string path)
        {
            Directory.CreateDirectory(path+"\\"+Name);
        }

        #endregion

    }
}
