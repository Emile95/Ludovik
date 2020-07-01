using Library.Class;
using Library.Plugins;
using Library.Plugins.Job;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        #region Properties

        #endregion

        public StandardJob()
        {
            ClassName = "StandardJob";
        }

        public sealed override string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + ClassName + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"description\":" + "\"" + Description + "\"," + "\n";

            jsonStr += depthTab + "\t" + "\"properties\":" + "[\n";

            for (int i = 1; i < Properties.Count; i++)
            {
                jsonStr += Properties[i].Item1.ToJson(true, nbTab + 2);
                jsonStr += (i < Properties.Count - 1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]" + "\n";

            jsonStr += "}";
            return jsonStr;
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
