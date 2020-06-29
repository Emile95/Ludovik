using Library.Class;
using Library.Plugins;
using Library.Plugins.Job;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        #region Properties

        public string Label { get; set; }

        #endregion

        public StandardJob()
        {
            Properties = new List<Property>();
        }

        #region Job implementation

        public sealed override void PreBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {
            loggers.GetLogger<JobBuildLogger.JobBuildLogger>()
                .Log(new Log("Job start at " + DateTime.Now));
        }

        public sealed override void Build(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {

        }

        public sealed override void AfterBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {
            loggers.GetLogger<StandardLogger.StandardLogger>()
                .Log(new Log("the job " + Name + " has finish to run", Log.Type.Info));
        }

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

        #region IConvertable Implementation

        public sealed override string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for(int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab+"{\n";
            jsonStr += depthTab+"\t" + "\"_class\":" + "\"" + GetType().ToString() + "\"," + "\n";
            jsonStr += depthTab+"\t" + "\"description\":" + "\"" + Description + "\"," + "\n";
            jsonStr += depthTab+"\t" + "\"label\":" + "\"" + Label + "\"," + "\n";

            jsonStr += depthTab +"\t" + "\"properties\":" + "[\n";

            for(int i = 0; i < Properties.Count; i++)
            {
                jsonStr += Properties[i].ToJson(true, nbTab + 2);
                jsonStr +=  (i < Properties.Count-1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]" + "\n";

            jsonStr += "}";
            return jsonStr;
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

        public sealed override void LoadConfig(Config config)
        {
            Name = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("name");
            Description = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("description");
            Label = config.GetParameterValue<LabelParameterDefinition.LabelParameterDefinition>("label");
        }

        #endregion

        #region IRepository Implementation

        public sealed override void CreateRepository(string path)
        {
            base.CreateRepository(path);

            string dirPath = path + "\\" + Name;

            string configFile = dirPath + "\\config.json";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(File.Create(configFile)))
            {
                file.WriteLine(ToJson(true));
            }

            //Create Builds Directory
            Directory.CreateDirectory(dirPath+"\\builds");
        }

        #endregion
    }
}
