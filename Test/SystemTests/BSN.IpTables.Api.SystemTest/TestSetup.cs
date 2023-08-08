using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Api.SystemTest
{
    [SetUpFixture]
    public class TestSetup
    {
        // TODO: Move to BSN.Commons
        public static int GetAvailablePort()
		{
            const ushort MIN_PORT = 3000;
            const ushort MAX_PORT = UInt16.MaxValue - 30;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            HashSet<int> usedPorts = Enumerable.Empty<int>()
                .Concat(ipProperties.GetActiveTcpConnections().Select(c => c.LocalEndPoint.Port))
                .Concat(ipProperties.GetActiveTcpListeners().Select(c => c.Port))
                .Concat(ipProperties.GetActiveUdpListeners().Select(c => c.Port))
                .ToHashSet();

            for (int port = MIN_PORT; port <= MAX_PORT; ++port)
			{
                if (!usedPorts.Contains(port)) return port;
			}

            throw new ConfigurationErrorsException("This system does not has any port for using int this application");
		}

        public static RestClient CreateRestClient(string serverAddress)
		{
            RestClient client = new RestClient(serverAddress);
            RestResponse response = client.Execute(request: new RestRequest("api"));
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                client = new RestClient(TestSetup.ServerUrl);
                if (client.Execute(request: new RestRequest("api")).ResponseStatus != ResponseStatus.Completed)
                    throw new ConfigurationErrorsException($"RestClient does not found server in address {TestSetup.ServerUrl}");
            }

            return client;
		}

        public static RestClient CreateDefaultRestClient()
        {
            return new RestClient($"{TestSetup.ServerUrl}");
        }

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = WebApplication.CreateBuilder();

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);
            builder.WebHost.UseUrls(ServerUrl);

            app = builder.Build();

            startup.Configure(app, app.Environment);

            app.StartAsync();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            app.StopAsync().Wait();
        }

        public static string ServerUrl
        {
            get;
        } = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? $"{DEFAULT_SERVERL_URL}:{GetAvailablePort()}";

        public const string DEFAULT_PREFIX_URL = "api/v1";
        private WebApplication app;
        private const string DEFAULT_SERVERL_URL = "http://localhost";


    }
}
