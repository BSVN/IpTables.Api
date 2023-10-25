using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using BSN.IpTables.Data;
using BSN.IpTables.Domain;

namespace BSN.IpTables.Api
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

            var app = builder.Build();

            startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}