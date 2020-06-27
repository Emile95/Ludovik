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

        [HttpGet("{name}/config")]
        public IActionResult GetConfig(string name)
        {
            return Ok();
        }

        [HttpPost("run")]
        public IActionResult Run([FromBody] JobRunSetting setting)
        {
            _jobApplication.RunJob(setting);
            return Ok();
        }
    }
}
