using Application.JobApplication.PostModel;
using Application.ThreadApplication;
using Library;
using Library.Class;
using Library.Plugins.Job;
using Library.StandardImplementation.StandardLogger;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Application.JobApplication
{
    public class JobApplication : IJobApplication
    {
        private readonly IThreadApplication _threadApplication;

        public JobApplication(
            IThreadApplication threadApplication
        )
        {
            _threadApplication = threadApplication;

            _threadApplication.AddInterval(2000, () => RunJob(new JobRunSetting() { Name = "job" }));
        }

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
