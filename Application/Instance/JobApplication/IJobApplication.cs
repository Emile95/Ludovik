using Application.JobApplication.PostModel;
using Application.SendedModel;

namespace Application.JobApplication
{
    public interface IJobApplication
    {
        void RunJob(JobRunModel model);

        void CancelRunningJob(CancelRunModel model);

        object GetRunningJobs();
    }
}
