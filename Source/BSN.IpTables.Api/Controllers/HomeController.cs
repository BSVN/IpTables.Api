using Microsoft.AspNetCore.Mvc;

namespace BSN.IpTables.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();

        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return Ok();
        }

        private readonly ILogger<HomeController> _logger;
    }
}
