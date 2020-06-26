using Library.Class;
using Library.Plugins.Job;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        #region Properties

        public string Label { get; set; }

        #endregion

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

        public sealed override void LoadFromConfig(Config config)
        {
            Name = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("name");
            Description = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("description");
            Label = config.GetParameterValue<LabelParameterDefinition.LabelParameterDefinition>("label");
        }

        #endregion

        #region AsbtractBuild implementation

        public sealed override void PreBuild(Build build, LoggerList loggers)
        {
            loggers.GetLogger<JobBuildLogger.JobBuildLogger>()
                .Log(new Log("Job start at " + DateTime.Now));
        }

        public sealed override void Build(Build build, LoggerList loggers)
        {
            
        }

        public sealed override void AfterBuild(Build build, LoggerList loggers)
        {
            loggers.GetLogger<StandardLogger.StandardLogger>()
                .Log(new Log("the job " + Name + " has finish to run", Log.Type.Info));
        }

        #endregion

        #region IRepositoryImplementation

        public sealed override void CreateRepository(string path)
        {
            base.CreateRepository(path);

            string dirPath = path + "\\" + Name;

            //Create Config File
            string jsonStr = "{\n";
            jsonStr += "\t" + "\"description\":" + "\"" +Description + "\"," + "\n";
            jsonStr += "\t" + "\"label\":" + "\"" + Label + "\"" + "\n";
            jsonStr += "}";

            string configFile = dirPath + "\\config.json";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(File.Create(configFile)))
            {
                file.WriteLine(jsonStr);
            }

            //Create Builds Directory
            Directory.CreateDirectory(dirPath+"\\builds");
        }

        #endregion
    }
}
