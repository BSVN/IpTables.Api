/**
 * The partial class Module is used to extend or modify the behavior of the AutoRest-generated classes and methods.
 * Here we are trying to get the environmental variable server address and set to request of all URLs in SendAsync method.
 */

using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSN.IpTables.V1
{
    public partial class Module
    {
        /**
         * Pipeline Modification: The Module class contains methods (AfterCreatePipeline and BeforeCreatePipeline) that are
         * involved in the creation of an HTTP pipeline (HttpPipeline).
         * This pipeline is used for handling HTTP requests and responses.
         */
        partial void AfterCreatePipeline(
            global::System.Management.Automation.InvocationInfo invocationInfo,
            ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline
        )
        {
            if (pipeline == null)
                throw new NullReferenceException("Pipeline is null!");
            pipeline.Append(SendAsync);
        }

        partial void BeforeCreatePipeline(
            global::System.Management.Automation.InvocationInfo invocationInfo,
            ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline
        ) { }

        /**
         * The SendAsync method is involved in processing HTTP requests. It uses the 
         * GetIptableServerAddressAsync method to obtain a server address, modifies the request URI accordingly,
         * and then delegates to the next step in the pipeline.
         */
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(
            System.Net.Http.HttpRequestMessage request,
            BSN.IpTables.V1.Runtime.IEventListener callback,
            BSN.IpTables.V1.Runtime.ISendAsync next
        )
        {
            string serverAddress = Environment.GetEnvironmentVariable("ServerAddress").ToString();
            if (serverAddress == null)
            {
                throw new ArgumentNullException(
                    nameof(serverAddress),
                    "ServerAddress variable is not set."
                );
            }
            string requestUriString = request.RequestUri.ToString();
            Uri newUri = new Uri(requestUriString);
            string host = newUri.Host;
            string finalUrl = requestUriString.Replace(host, serverAddress);
            request.RequestUri = new Uri(finalUrl);
            if (next == null)
                throw new NullReferenceException("Next is null!");

            return await next.SendAsync(request, callback);
        }

        partial void CustomInit() { }
    }
}
