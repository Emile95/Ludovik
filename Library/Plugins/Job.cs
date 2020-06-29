using Library.Class;
using Library.Interface;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Plugins.Job
{
    public abstract class Job : IBuildable, ILoadable, IConfigurable, IRunnable, IRepository
    {
        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public Trigger[] Triggers { get; set; }

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

        #region IBuildable Implementation

        public virtual void PreBuild(Build build, LoggerList loggers) { }

        public virtual void Build(Build build, LoggerList loggers) { }

        public virtual void AfterBuild(Build build, LoggerList loggers) { }

        #endregion

        #region IRunnable Implementation

        public void Run(LoggerList loggers)
        {
            Build build = new Build(1,"#1","");
            build.CreateRepository("jobs\\"+Name+"\\builds");

            loggers.AddLogger(new JobBuildLogger(Name,build.Number));

            PreBuild(build, loggers);
            Build(build, loggers);
            AfterBuild(build, loggers);
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
