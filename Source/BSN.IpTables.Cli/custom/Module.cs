using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSN.IpTables.V1
{
    public partial class Module
    {
        partial void AfterCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo,
            ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {
            if (pipeline == null)
                throw new NullReferenceException("Pipeline is null!");
            pipeline.Append(SendAsync);
        }

        partial void BeforeCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo,
            ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage>
           SendAsync(System.Net.Http.HttpRequestMessage request,
            BSN.IpTables.V1.Runtime.IEventListener callback,
            BSN.IpTables.V1.Runtime.ISendAsync next)
        {
            string serverAddress = Environment.GetEnvironmentVariable("ServerAddress").ToString();
            string requestUriString = request.RequestUri.ToString();
            Uri newUri = new Uri(requestUriString);   
            string host = newUri.Host; 
            string finalUrl = requestUriString.Replace(host,serverAddress); 
            request.RequestUri = new Uri(finalUrl);
            if (next == null)
                throw new NullReferenceException("Next is null!");

            return await next.SendAsync(request, callback);
        }

        partial void CustomInit()
        {
        }
    }
}