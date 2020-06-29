using Application.JobApplication.PostModel;
using Application.SendedModel;

namespace Application.JobApplication
{
    public interface IJobApplication
    {
        void RunJob(JobRunSetting setting);

        void CancelRunningJob(CancelRunModel model);

        object GetRunningJobs();
    }
}
