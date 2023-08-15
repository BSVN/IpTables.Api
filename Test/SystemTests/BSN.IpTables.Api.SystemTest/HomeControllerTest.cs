using BSN.IpTables.Api.Controllers.V1;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1.InputModels;
using BSN.IpTables.Presentation.Dto.V1.Requests;
using Cysharp.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
            client = factory.WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(@"Source/BSN.IpTables.Api")
                ).CreateClient();
        }

        [SetUp]
        public void Initialize()
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
            var ruleParameter = new RuleInputModel()
            {
                Protocol = "tcp",
                SourceIp = "2.2.2.2",
                DestinationIp = "1.1.1.1",
                SourcePort = "111",
                DestinationPort = "222",
                Jump = "DROP"
            };

            var parameter = new RulesCommandServiceAppendRequest()
            {
                Chain = Chain.INPUT
            };

            // TODO: This ugly method for https://github.com/Cysharp/WebSerializer#nested-type-and-nameprefix

            var writer = new WebSerializerWriter();
            WebSerializer.ToQueryString(writer, parameter);
            writer.AppendConcatenate();
            writer.NamePrefix = $"{nameof(parameter.Data)}.";
            WebSerializer.ToQueryString(writer, ruleParameter);
            string request = $"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}/Append?{writer.GetStringBuilder()}";
            HttpResponseMessage response = await client.PostAsync(request, new WebSerializerFormUrlEncodedContent(writer));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public const string DEFAULT_PREFIX_URL = "api/v1";

        private HttpClient client;
        private WebApplicationFactory<Program> factory;
        private const string HOME_CONTROLLER_ROUTE_PREFIX = "Home";
        private const string SAMPLE_RULE = "-p tcp ! -f -j DROP --sport 99";
    }
}