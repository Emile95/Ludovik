using Library.Interface;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Plugins.Job
{
    public abstract class Job : IUserItem
    {
        public string Name { get; protected set; }
        public string Description {  get; protected set; }
        public virtual void LoadFromConfig(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");
        }
    }
}
