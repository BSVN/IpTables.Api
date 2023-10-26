using BSN.Commons.Responses;
using BSN.IpTables.Api.Controllers.V1;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1.InputModels;
using BSN.IpTables.Presentation.Dto.V1.Requests;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
using Cysharp.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace BSN.IpTables.Api.SystemTest
{
    [TestFixture("")]
    [TestFixture("/rules")]
    [TestFixture(HomeControllerTest.HOME_CONTROLLER_ROUTE_PREFIX)]
    public class HomeControllerTest
    {
        public HomeControllerTest(string controllerRoutePrefix)
        {
            sampleRule = new RuleInputModel()
            {
                Protocol = "tcp",
                SourceIp = "2.2.2.2",
                DestinationIp = "1.1.1.1",
                SourcePort = "111",
                DestinationPort = "222",
                Jump = "DROP"
            };

            this.controllerRoutePrefix = controllerRoutePrefix;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            factory = new WebApplicationFactory<Program>();
            client = factory.WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(@"Source/BSN.IpTables.Api")
                ).CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            factory.Dispose();
            client.Dispose();
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
            // Act
            HttpResponseMessage response = await client.GetAsync($"{DEFAULT_PREFIX_URL}{controllerRoutePrefix}/List");
            Response<IpTablesChainSetViewModel> content = await response.Content.ReadFromJsonAsync<Response<IpTablesChainSetViewModel>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeNull();
        }

        [Test]
        [Order(1)]
        public async Task AppendWithNormalRule_ShouldBeOk()
        {
            // Arrange
            var parameter = new RulesCommandServiceAppendRequest()
            {
                Chain = Chain.INPUT,
                Rule = sampleRule
            };

            string request = $"{DEFAULT_PREFIX_URL}{controllerRoutePrefix}/Append?{SystemTest.Helpers.FlattenObject(parameter).ToQueryString()}";
            int numberOfRulesBeforeAppending = (await GetRules()).Count();

            // Act
            HttpResponseMessage response = await client.PostAsync(request, new StringContent(SystemTest.Helpers.FlattenObject(parameter).ToQueryString()));
            int numberOfRulesAfterAppending = (await GetRules()).Count();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            numberOfRulesAfterAppending.Should().BeGreaterThan(numberOfRulesBeforeAppending);
        }

        [Test]
        [Order(2)]
        public async Task DeleteWithPreviousExistRule_ShouldBeOk()
        {
            // Arrange
            var parameter = new RulesCommandServiceDeleteRequest()
            {
                Chain = Chain.INPUT,
                Rule = sampleRule
            };

            string request = $"{DEFAULT_PREFIX_URL}{controllerRoutePrefix}?{SystemTest.Helpers.FlattenObject(parameter).ToQueryString()}";
            int numberOfRulesBeforeDeleting = (await GetRules()).Count();

            // Act
            HttpResponseMessage response = await client.DeleteAsync(request);
            int numberOfRulesAfterDeleting = (await GetRules()).Count();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            numberOfRulesAfterDeleting.Should().BeLessThan(numberOfRulesBeforeDeleting);
        }

        public const string DEFAULT_PREFIX_URL = "api/v1";

        private async Task<IEnumerable<IpTablesRuleViewModel>> GetRules()
        {
            HttpResponseMessage response = await client.GetAsync($"{DEFAULT_PREFIX_URL}{controllerRoutePrefix}/List");
            Response<IpTablesChainSetViewModel> content = await response.Content.ReadFromJsonAsync<Response<IpTablesChainSetViewModel>>();

            return (
                from chains in content.Data.IpTablesChains
                from rule in chains.Rules
                select rule
             ).ToList();
        }

        private HttpClient client;
        private WebApplicationFactory<Program> factory;
        private readonly RuleInputModel sampleRule;
        private readonly string controllerRoutePrefix;

        private const string HOME_CONTROLLER_ROUTE_PREFIX = "/Home";
        private const string SAMPLE_RULE = "-p tcp ! -f -j DROP --sport 99";
    }
}