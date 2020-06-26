using Library.Class;
using Library.Interface;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Plugins.Job
{
    public abstract class Job : ILoadable, IConfigurable
    {
        public string Name { get; protected set; }

        public string Description {  get; protected set; }

        public virtual void LoadFromFolder(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");
        }

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
    }
}
