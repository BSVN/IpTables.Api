using BSN.IpTables.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BSN.IpTables.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILoggerFactory loggerFactory, IIpTablesSystem system)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _system = system;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        private readonly ILogger<HomeController> _logger;
        private readonly IIpTablesSystem _system;
    }
}
