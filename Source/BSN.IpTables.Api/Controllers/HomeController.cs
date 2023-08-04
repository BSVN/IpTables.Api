using BSN.IpTables.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BSN.IpTables.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(ILoggerFactory loggerFactory, IIpTablesSystem system)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _system = system;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return Ok();
        }

        private readonly ILogger<HomeController> _logger;
        private readonly IIpTablesSystem _system;
    }
}
