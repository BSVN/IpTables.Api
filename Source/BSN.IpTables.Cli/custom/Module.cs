using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSN.IpTables.V1
{
    public partial class Module
    {
        partial void AfterCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo, ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {            
            if (pipeline == null)
                Console.WriteLine("##### Module::BeforeCreatePipeline @@@@@ pipeline is null!");
            pipeline.Append(SendAsync);
        }

        partial void BeforeCreatePipeline(global::System.Management.Automation.InvocationInfo invocationInfo, ref BSN.IpTables.V1.Runtime.HttpPipeline pipeline)
        {
        }
	
	     public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, BSN.IpTables.V1.Runtime.IEventListener callback, BSN.IpTables.V1.Runtime.ISendAsync next)
        {
			string jsonFilePath = @"D:\Iptables\IpTables.Api\Source\BSN.IpTables.Cli\serverAddress.txt";
            string serverAddress = await LoadServerAddressAsync(jsonFilePath);
            string requestUriString = request.RequestUri.ToString();

            // Find the indices of the first and second "/" in the RequestUri
			int initialBackslashIndex = requestUriString.IndexOf('/');
            int firstBackslashIndex = requestUriString.IndexOf('/', initialBackslashIndex + 1);
            int secondBackslashIndex = requestUriString.IndexOf('/', initialBackslashIndex + 2);

            // Check if both backslashes are found
            if (firstBackslashIndex != -1 && secondBackslashIndex != -1)
            {
                // Extract the substring between the first and second "\"
                string uriSubstring = serverAddress.Trim();
				string uriFinal = uriSubstring.ToString();
				string firstString = requestUriString.Substring(0, firstBackslashIndex + 1);
				string secondString = requestUriString.Substring(secondBackslashIndex);
                string modifiedUriString = firstString + uriFinal + secondString;
                // Replace the original substring with the modified substring in the RequestUri
                request.RequestUri = new Uri(modifiedUriString);
            }
            else
            {
                // If backslashes are not found, handle the error or take appropriate action
                Console.WriteLine("##### Backslashes not found in the RequestUri.");
            }

            if (next == null)
                Console.WriteLine("##### Module::SendAsync @@@@@ next is null!");

            return await next.SendAsync(request, callback);
        }

        partial void CustomInit()
        {
        }

public async Task<string> LoadServerAddressAsync(string filePath)
{
    try
    {
        // Read the text file content
        string fileContent = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(fileContent))
        {
            return "http://192.168.21.67:8080";
        }

        // Trim any leading or trailing whitespaces
        string serverAddress = fileContent.Trim();
        return serverAddress;
    }
    catch (Exception )
    {
        return "http://192.168.21.67:8080";
    }
}
    }
}