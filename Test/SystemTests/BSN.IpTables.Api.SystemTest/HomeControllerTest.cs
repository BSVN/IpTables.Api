using BSN.IpTables.Api.Controllers.V1;
using FluentAssertions;
using RestSharp;
using System.Net;

namespace BSN.IpTables.Api.SystemTest
{
    [TestFixture]
    public class HomeControllerTest 
    {
        [SetUp]
        public void Initialize()
        {
            client = TestSetup.CreateDefaultRestClient();
        }

        [Test]
        public void List_ShouldBeOk()
        {
            RestRequest request = new RestRequest($"{TestSetup.DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}");
            RestResponse response = client.Get(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private RestClient client;
        private const string HOME_CONTROLLER_ROUTE_PREFIX = "Home";
    }
}