using Library.Class;
using Library.Plugins.Job;
using Library.StandardImplementation.StandardJob;
using Library.StandardImplementation.StandardLogger;

namespace Application.JobApplication
{
    public class JobApplication : IJobApplication
    {
        #region IJobApplication Implementation

        public void RunJob(string name)
        {
            Job job = new StandardJob();

            job.LoadFromFolder("jobs",name);

            LoggerList loggers = new LoggerList();
            loggers.AddLogger(new StandardLogger());

            job.Run(loggers);
        }

        #endregion
    }
}
