using Library.Class;
using Library.Plugins.Job;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        public string Label { get; private set; }

        public sealed override void LoadFromFolder(string path, string folderName)
        {
            base.LoadFromFolder(path, folderName);

            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Label = configFileObject.Value<string>("label");
        }

        public sealed override Config GetConfig()
        {
            Config config = base.GetConfig();
            config.AddParameter(
                Label,
                new LabelParameterDefinition.LabelParameterDefinition("Label", "Label used for this job")
            );

            return config;
        }

        public sealed override void SaveConfig(Config config)
        {
            
        }
    }
}
