using Library.Class;
using Library.Plugins.Job;
using Library.Plugins.Logger;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        public string Label { get; private set; }

        #region ILoadable implementation

        public sealed override void LoadFromFolder(string path, string folderName)
        {
            base.LoadFromFolder(path, folderName);

            string configFile = File.ReadAllText(path + "\\" + folderName + "\\config.json");
            JObject configFileObject = JObject.Parse(configFile);

            Label = configFileObject.Value<string>("label");
        }

        #endregion

        #region IConfigurable implementation

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

        #endregion

        #region AsbtractBuild implementation

        public sealed override void PreBuild(Logger logger)
        {
            
        }

        public sealed override void Build(Logger logger)
        {
            
        }

        public sealed override void AfterBuild(Logger logger)
        {
            logger.Log("the job " + Name + " has finish to run");
        }

        #endregion
    }
}
