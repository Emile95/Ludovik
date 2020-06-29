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
        private readonly ThreadApplication.ThreadApplication _threadApplication;

        public JobApplication(
            ThreadApplication.ThreadApplication threadApplication
        )
        {
            _threadApplication = threadApplication;

            //_threadApplication.AddInterval("job#1", 2000, () => RunJob(new JobRunSetting() { Name = "job" }));
        }

        #region IJobApplication Implementation

        public void RunJob(JobRunSetting setting)
        {
            JObject jObject = JObject.Parse(File.ReadAllText("jobs\\" + setting.Name + "\\config.json"));

            Job job = PluginStorage.CreateJob(jObject.Value<string>("_class"));

            job.LoadFromFolder("jobs", setting.Name);

            LoggerList loggers = new LoggerList();
            loggers.AddLogger(new StandardLogger());

            _threadApplication.AddRun(setting.Name, job, loggers);
        }

        public object GetRunningJobs()
        {
            return _threadApplication.GetRuns<Job>();
        }

        #endregion
    }
}
