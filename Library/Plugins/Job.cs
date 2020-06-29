using Library.Class;
using Library.Interface;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;

namespace Library.Plugins.Job
{
    public abstract class Job : ILoadable, IConfigurable, IRunnable, IRepository
    {
        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public Trigger[] Triggers { get; set; }

        #endregion

        public abstract void Build(Build build, CancellationToken taskCancelToken, LoggerList loggers);

        public virtual void PreBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers) { }

        public virtual void AfterBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers) { }

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

        #region IConfigurable implementation

        public virtual Config GetConfig()
        {
            string configFile = File.ReadAllText("jobs\\"+Name + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Config config = new Config();
            config.AddParameter(
                Name,
                new StringParameterDefinition("Name","Name of this job")
            );
            config.AddParameter(
                configFileObject.Value<string>("description"),
                new StringParameterDefinition("Description", "Description of this job")
            );

            return config;
        }

        public abstract void SaveConfig(Config config);

        public abstract void LoadFromConfig(Config config);

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
