using Library.Class;
using Library.Interface;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Plugins.Job
{
    public abstract class Job : IBuildable, ILoadable, IConfigurable, IRunnable
    {
        #region Properties

        public string Name { get; protected set; }

        public string Description {  get; protected set; }

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

        #endregion

        #region IBuildable Implementation

        public virtual void PreBuild(Logger.Logger logger) { }
        public virtual void Build(Logger.Logger logger) { }
        public virtual void AfterBuild(Logger.Logger logger) { }

        #endregion

        #region IRunnable Implementation

        public void Run(Logger.Logger logger)
        {
            PreBuild(logger);
            Build(logger);
            AfterBuild(logger);
        }

        #endregion
    }
}
