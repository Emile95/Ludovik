using Application.JobApplication;
using Application.JobApplication.PostModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("job")]
    public class JobController : ControllerBase
    {
        private readonly IJobApplication _jobApplication;

        public JobController(
            IJobApplication jobApplication
        )
        {
            _jobApplication = jobApplication;
        }

        [HttpGet("running")]
        public IActionResult GetRunningJobs()
        {
            return Ok(_jobApplication.GetRunningJobs());
        }

        [HttpPost("run")]
        public IActionResult Run([FromBody] JobRunSetting setting)
        {
            _jobApplication.RunJob(setting);
            return Ok();
        }
    }
}
