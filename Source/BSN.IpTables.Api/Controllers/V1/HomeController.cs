using AutoMapper;
using BSN.Commons.PresentationInfrastructure;
using BSN.Commons.Responses;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1.Requests;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
using IPTables.Net.Iptables;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BSN.IpTables.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]

    [Route("api/v{version:apiVersion}/rules")]
    [Route("api/v{version:apiVersion}")]
    public class HomeController : ControllerBase
    {
        // TODO: Check exception and error state for all methods (actions)

        public HomeController(ILoggerFactory loggerFactory, IMapper mapper, IIpTablesSystem ipTables)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
            this.mapper = mapper;
            this.ipTables = ipTables;
        }

        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(Response<IpTablesChainSetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Response<IpTablesChainSetViewModel>>> List()
        {
            logger.LogInformation("HomeController: List is called");
            var response = new Response<IpTablesChainSetViewModel>()
            {
                StatusCode = ResponseStatusCode.OK,
                Data = mapper.Map<IpTablesChainSetViewModel>(ipTables.List())
            };
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPut]
        [Route("Insert")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Response>> Insert([FromQuery] RulesCommandServiceInsertRequest insert_request)
        {
            // Input parameter name could not be "request" because it cause error in Autorest code generation.

            logger.LogInformation("HomeController: Insert is called");
            var response = new Response()
            {
                StatusCode = ResponseStatusCode.OK
            };
            return new JsonResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }

        [HttpPost]
        [Route("Append")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Response>> Append([FromQuery] RulesCommandServiceAppendRequest append_request)
        {
            logger.LogInformation("HomeController: Append is called");
            var chains = new IpTablesChainSet((int)IpVersion.V4);

            IpTablesRule rule = IpTablesRule.Parse($"-A {append_request.Chain} {append_request.Rule}", null, chains);
            // TODO: Check exception
            ipTables.AppendRule(rule);

            var response = new Response()
            {
                StatusCode = ResponseStatusCode.OK
            };

            return new JsonResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Response>> Delete([FromQuery] RulesCommandServiceDeleteRequest delete_request)
        {
            logger.LogInformation("HomeController: Delete is called");
            var chains = new IpTablesChainSet((int)IpVersion.V4);

            IpTablesRule rule = IpTablesRule.Parse($"-A {delete_request.Chain} {delete_request.Rule}", null, chains);
            // TODO: Check exception
            ipTables.DeleteRule(rule);

            var response = new Response()
            {
                StatusCode = ResponseStatusCode.OK
            };

            return new JsonResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }

        private readonly ILogger<HomeController> logger;
        private readonly IMapper mapper;
        private readonly IIpTablesSystem ipTables;
    }
}
