using Application.JobApplication.PostModel;
using Application.SendedModel;
using Library;
using Library.Class;
using Library.Plugins.Job;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.StandardLogger;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Application.JobApplication
{
    public class JobApplication : IJobApplication
    {
        #region Properties and Construcotr

        private readonly ThreadApplication.ThreadApplication _threadApplication;

        public JobApplication(
            ThreadApplication.ThreadApplication threadApplication
        )
        {
            _threadApplication = threadApplication;
        }

        #endregion

        #region IJobApplication Implementation

        public void CreateJob(JobConfigModel model)
        {
            //Instanciate plugins depending on class name
            Job job = PluginStorage.CreateObject<Job>(model.ClassName);

            //Load job from config
            job.LoadConfig(model.GetConfig());

            //Create repository configuration in jobs folder
            job.CreateRepository("jobs");
        }

        public void RunJob(JobRunModel model)
        {
            //Instanciate plugins depending on class name
            Job job = PluginStorage.CreateObject<Job>(
                //Go Fetch class name from config file
                JObject.Parse(File.ReadAllText("jobs\\" + model.Name + "\\config.json")).Value<string>("_class")
            );

            //Load job from folder configuration
            job.LoadFromFolder("jobs", model.Name);

            //Create Logger list for the run
            LoggerList loggers = new LoggerList();
            //Add Standard logger in the logger list
            loggers.AddLogger(new StandardLogger());

            //Add job build in the process with his name as key and provide log
            _threadApplication.AddRun(model.Name, job, loggers);
        }

        public void CancelRunningJob(CancelRunModel model)
        {
            _threadApplication.CancelRun(model.Key);
        }

        public object GetRunningJobs()
        {
            return _threadApplication.GetRuns<Job>();
        }

        #endregion
    }
}
