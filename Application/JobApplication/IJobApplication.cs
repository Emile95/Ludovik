using Application.JobApplication.PostModel;

namespace Application.JobApplication
{
    public interface IJobApplication
    {
        void RunJob(JobRunSetting setting);
    }
}
