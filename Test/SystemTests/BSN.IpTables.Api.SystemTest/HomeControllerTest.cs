using BSN.IpTables.Api.Controllers.V1;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1.InputModels;
using BSN.IpTables.Presentation.Dto.V1.Requests;
using Cysharp.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Runtime.CompilerServices;

namespace BSN.IpTables.Api.SystemTest
{
    [TestFixture]
    public class HomeControllerTest 
    {
        public HomeControllerTest()
        {
            sampleRule = new RuleInputModel()
            {
//                Protocol = "tcp",
   //             SourceIp = "2.2.2.2",
                DestinationIp = "1.1.1.1",
 //               SourcePort = "111",
  //              DestinationPort = "222",
                Jump = "DROP"
            };
        }


        [OneTimeSetUp]
        public void Setup()
        {
            factory = new WebApplicationFactory<Program>();
            client = factory.WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(@"Source/BSN.IpTables.Api")
                ).CreateClient();
        }

        [SetUp]
        public void Initialize()
        {
        }

        [TearDown]
        public void Cleanup()
        {

        }

        [Test]
        public async Task List_ShouldBeOk()
        {
            HttpResponseMessage response = await client.GetAsync($"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}/List");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task AppendWithNormalRule_ShouldBeOk()
        {
            // Arrange
            var parameter = new RulesCommandServiceAppendRequest()
            {
                Chain = Chain.INPUT,
                Data = sampleRule
            };

            string request = $"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}/Append?{SystemTest.Helpers.FlattenObject(parameter).ToQueryString()}";

            // Act
            HttpResponseMessage response = await client.PostAsync(request, new StringContent(SystemTest.Helpers.FlattenObject(parameter).ToQueryString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task DeleteWithPreviousExistRule_ShouldBeOk()
        {
            // Arrange
            var parameter = new RulesCommandServiceDeleteRequest()
            {
                Chain = Chain.INPUT,
                Data = sampleRule
            };

            string request = $"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}?{SystemTest.Helpers.FlattenObject(parameter).ToQueryString()}";

            // Act
            HttpResponseMessage response = await client.DeleteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public const string DEFAULT_PREFIX_URL = "api/v1";

        private HttpClient client;
        private WebApplicationFactory<Program> factory;
        private readonly RuleInputModel sampleRule;

        private const string HOME_CONTROLLER_ROUTE_PREFIX = "Home";
        private const string SAMPLE_RULE = "-p tcp ! -f -j DROP --sport 99";
    }
}