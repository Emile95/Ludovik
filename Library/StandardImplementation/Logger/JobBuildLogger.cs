using Library.Class;
using Library.Plugins.Logger;

namespace Library.StandardImplementation.JobBuildLogger
{
    public class JobBuildLogger : Logger
    {
        private readonly string _jobName;
        private readonly int _buildNumber;

        public JobBuildLogger(string jobName, int buildNumber)
        {
            _jobName = jobName;
            _buildNumber = buildNumber;
        }

        protected sealed override string GetFilePath()
        {
            return "jobs\\"+ _jobName + "\\builds\\" + _buildNumber + "\\console.log";
        }

        protected sealed override string GetLogLine(Log log)
        {
            return log.Message;
        }
    }
}
