using System;

namespace BSN.IpTables.V1
{
    public partial class Module
    {
        partial void AfterCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo, ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {
            Console.WriteLine("##### Module::AfterCreatePipeline @@@@@");
            Console.WriteLine("##### Module::AfterCreatePipeline @@@@@ " + invocationInfo.ToString() + " @@@@ " + pipeline.ToString());
        }

        partial void BeforeCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo, ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {
            Console.WriteLine("##### Module::BeforeCreatePipeline @@@@@");

            if (pipeline == null)
                Console.WriteLine("##### Module::BeforeCreatePipeline @@@@@ pipeline is null!");
            pipeline.Append(SendAsync);
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, BSN.IpTables.V1.Runtime.IEventListener callback, BSN.IpTables.V1.Runtime.ISendAsync next)
        {
            Console.WriteLine("##### Module::SendAsync @@@@@");
            Console.WriteLine("##### Module::SendAsync @@@@@" + " " + request.RequestUri);

            // FIXME: replace URI with server address loaded by loadServerAddress' method
            
            if (next == null)
                Console.WriteLine("##### Module::SendAsync @@@@@ next is null!");
            return await next.SendAsync(request, callback);
        }

        partial void CustomInit()
        {
            Console.WriteLine("##### Module::CustomInit @@@@@");
        }

        public string loadServerAddress()
        {
            // FIXME: Load server address from saved place
            return "192.168.21.123:1234";
        }
    }
}
