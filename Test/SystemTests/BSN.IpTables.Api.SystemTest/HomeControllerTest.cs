using BSN.IpTables.Api.Controllers.V1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace BSN.IpTables.Api.SystemTest
{
    [TestFixture]
    public class HomeControllerTest 
    {
        [OneTimeSetUp]
        public void Setup()
        {
            factory = new WebApplicationFactory<Program>();
            client = factory.CreateClient();
        }

        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public async Task List_ShouldBeOk()
        {
            HttpResponseMessage response = await client.GetAsync($"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private HttpClient client;
        private WebApplicationFactory<Program> factory;
        private const string HOME_CONTROLLER_ROUTE_PREFIX = "Home";
        public const string DEFAULT_PREFIX_URL = "api/v1";
    }
}