using Application.JobApplication.PostModel;
using Library;
using Library.Class;
using Library.Plugins.Job;
using Library.StandardImplementation.StandardJob;
using Library.StandardImplementation.StandardLogger;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Application.JobApplication
{
    public class JobApplication : IJobApplication
    {
        #region IJobApplication Implementation

        public void RunJob(JobRunSetting setting)
        {
            JObject jObject = JObject.Parse(File.ReadAllText("jobs\\" + setting.Name + "\\config.json"));

            Job job = PluginStorage.CreateJob(jObject.Value<string>("_class"));

            job.LoadFromFolder("jobs", setting.Name);

            LoggerList loggers = new LoggerList();
            loggers.AddLogger(new StandardLogger());

            job.Run(loggers);
        }

        #endregion
    }
}
