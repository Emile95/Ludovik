using Library.Class;
using Library.Interface;
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

        public abstract Config GetConfig();

        public abstract void SaveConfig(Config config);
    }
}
