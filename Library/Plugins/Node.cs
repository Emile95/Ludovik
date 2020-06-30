using Library.Class;
using Library.Interface;
using Library.StandardImplementation.StringParameterDefinition;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Plugins.Node
{
    public abstract class Node : ILoadable, IConfigurable, IRepository
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Labels { get; protected set; }

        #region ILoadable implementation

        public virtual void LoadFromFolder(string path, string folderName)
        {
            //Config.json object
            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Name = folderName;
            Description = configFileObject.Value<string>("description");
            Labels = configFileObject.Value<string>("labels");
        }

        #endregion

        #region IConfigurable implementation

        public virtual Config GetConfig()
        {
            string configFile = File.ReadAllText("nodes\\" + Name + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Config config = new Config();
            /*config.AddParameter(
                Name,
                new StringParameterDefinition("name", "Name of this node")
            );
            config.AddParameter(
                configFileObject.Value<string>("description"),
                new StringParameterDefinition("description", "Description of this node")
            );
            config.AddParameter(
                configFileObject.Value<string>("labels"),
                new StringParameterDefinition("labels", "Labels attach to this node")
            );
            */
            return config;
        }

        public abstract void SaveConfig(Config config);

        public abstract void LoadConfig(Config config);

        #endregion

        #region IRepository Implementation

        public virtual void CreateRepository(string path)
        {
            Directory.CreateDirectory(path + "\\" + Name);
        }

        #endregion
    }
}
