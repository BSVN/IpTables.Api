public partial class Module
{
    public partial class BsnIPTablesCli
    {
        public static void UpdateSwaggerJson(string serverAddress)
        {
            // Your existing code for updating Swagger JSON
            // ...

            // Now, you can use the 'serverAddress' variable in your Append method
        }

        public async global::System.Threading.Tasks.Task Append(string chain, string ruleInterfaceName, string ruleProtocol, string ruleSourceIP, string ruleDestinationIP, string ruleSourcePort, string ruleDestinationPort, string ruleJump, global::System.Func<global::System.Net.Http.HttpResponseMessage, global::System.Threading.Tasks.Task<BSN.IpTables.V1.Models.IResponse>, global::System.Threading.Tasks.Task> onOk, BSN.IpTables.V1.Runtime.IEventListener eventListener, BSN.IpTables.V1.Runtime.ISendAsync sender)
        {
            // Your existing Append method
            // Use the 'serverAddress' variable wherever needed
            var pathAndQuery = global::System.Text.RegularExpressions.Regex.Replace(
                "/api/v1/rules/Append"
                + "?"
                + (string.IsNullOrEmpty(chain) ? global::System.String.Empty : "Chain=" + global::System.Uri.EscapeDataString(chain))
                + "&"
                + (string.IsNullOrEmpty(ruleInterfaceName) ? global::System.String.Empty : "Rule.InterfaceName=" + global::System.Uri.EscapeDataString(ruleInterfaceName))
                + "&"
                + (string.IsNullOrEmpty(ruleProtocol) ? global::System.String.Empty : "Rule.Protocol=" + global::System.Uri.EscapeDataString(ruleProtocol))
                + "&"
                + (string.IsNullOrEmpty(ruleSourceIP) ? global::System.String.Empty : "Rule.SourceIp=" + global::System.Uri.EscapeDataString(ruleSourceIP))
                + "&"
                + (string.IsNullOrEmpty(ruleDestinationIP) ? global::System.String.Empty : "Rule.DestinationIp=" + global::System.Uri.EscapeDataString(ruleDestinationIP))
                + "&"
                + (string.IsNullOrEmpty(ruleSourcePort) ? global::System.String.Empty : "Rule.SourcePort=" + global::System.Uri.EscapeDataString(ruleSourcePort))
                + "&"
                + (string.IsNullOrEmpty(ruleDestinationPort) ? global::System.String.Empty : "Rule.DestinationPort=" + global::System.Uri.EscapeDataString(ruleDestinationPort))
                + "&"
                + (string.IsNullOrEmpty(ruleJump) ? global::System.String.Empty : "Rule.Jump=" + global::System.Uri.EscapeDataString(ruleJump)),
                "\\?&*$|&*$|(\\?)&+|(&)&+","$1$2");

await eventListener.Signal(
    BSN.IpTables.V1.Runtime.Events.URLCreated,
    CancellationToken.None,
    (createMessage) => new EventData(createMessage, "optional data")
);

var _url = new global::System.Uri($"http://{serverAddress}:8080{pathAndQuery}");
var request = new global::System.Net.Http.HttpRequestMessage(BSN.IpTables.V1.Runtime.Method.Post, _url);

await eventListener.Signal(
    BSN.IpTables.V1.Runtime.Events.RequestCreated,
    request.RequestUri.PathAndQuery,
    (createMessage) => new EventData(createMessage, "optional data")
);

await eventListener.Signal(
    BSN.IpTables.V1.Runtime.Events.HeaderParametersAdded,
    CancellationToken.None,
    (createMessage) => new EventData(createMessage, "optional data")
);

// Rest of your code...

            // make the call
            await this.Append_Call(request, onOk, eventListener, sender);
        }
    }
}
