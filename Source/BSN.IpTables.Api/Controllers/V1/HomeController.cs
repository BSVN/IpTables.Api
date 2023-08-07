using BSN.Commons.Responses;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BSN.IpTables.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILoggerFactory loggerFactory, IIpTablesSystem ipTables)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
            this.ipTables = ipTables;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IpTablesChainSetViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult List()
        {
            return Ok(ipTables.List());
        }

        private readonly ILogger<HomeController> logger;
        private readonly IIpTablesSystem ipTables;
    }
}
