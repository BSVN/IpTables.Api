using Microsoft.AspNetCore.Mvc;

namespace BSN.IpTables.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
