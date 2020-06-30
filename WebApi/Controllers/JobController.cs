using Application.JobApplication;
using Application.JobApplication.PostModel;
using Application.SendedModel;
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

        [HttpPost("cancelRun")]
        public IActionResult CancelRunningJob([FromBody] CancelRunModel model)
        {
            _jobApplication.CancelRunningJob(model);
            return Ok();
        }

        [HttpPost("run")]
        public IActionResult Run([FromBody] JobRunModel model)
        {
            _jobApplication.RunJob(model);
            return Ok();
        }
    }
}
