using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace hangfire_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        
        private readonly ILogger<DemoController> _logger;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly DemoJobs _demoJobs;

        public DemoController(ILogger<DemoController> logger, IBackgroundJobClient backgroundJobs, DemoJobs demoJobs)
        {
            _logger = logger;
            _backgroundJobs = backgroundJobs;
            _demoJobs = demoJobs;
        }
        
        [HttpPost("[action]")]
        public ActionResult<long?> SendEmail()
        {
            var rnd = new Random();
            var randomId = rnd.Next(1, 100);

            _backgroundJobs.Enqueue(() => _demoJobs.SendEmail(randomId));
            return randomId;
        }
        

    }
}