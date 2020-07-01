﻿using Application.JobApplication.PostModel;
using Application.SendedModel;
using Library;
using Library.Class;
using Library.Plugins.Job;
using Library.StandardImplementation.DescriptionPropertyDefinition;
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
        }

        #region IJobApplication Implementation

        public void CreateJob(JobConfigModel model)
        {
            Config config = new Config();
            config.AddProperty(
                new DescriptionPropertyDefinition(), 
                new string[] { 
                    model.Name,
                    model.Description
                }
            );

            Job job = PluginStorage.CreateObject<Job>(model.ClassName);
            job.LoadConfig(config);
            job.CreateRepository("jobs");
        }

        public void RunJob(JobRunModel model)
        {
            JObject jObject = JObject.Parse(File.ReadAllText("jobs\\" + model.Name + "\\config.json"));

            Job job = PluginStorage.CreateObject<Job>(jObject.Value<string>("_class"));

            job.LoadFromFolder("jobs", model.Name);

            LoggerList loggers = new LoggerList();
            loggers.AddLogger(new StandardLogger());

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
