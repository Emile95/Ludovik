using Application.JobApplication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("job")]
    public class JobController : Controller
    {
        #region Properties and Constructor

        private readonly IJobApplication _jobApplication;

        public JobController(
            IJobApplication jobApplication
        )
        {
            _jobApplication = jobApplication;
        }
        
        #endregion

        [HttpGet("run")]
        public IActionResult RunJob()
        {
            throw new System.Exception("asdasd");
            _jobApplication.RunJob("job");
            return Ok();
        }
    }
}
