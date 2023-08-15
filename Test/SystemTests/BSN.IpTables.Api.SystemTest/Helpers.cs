using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BSN.IpTables.Api.SystemTest
{

    /// <summary>
    /// <para>
    /// TODO: This ugly method for https://github.com/Cysharp/WebSerializer#nested-type-and-nameprefix
    /// If I want to use <c>WebSerilizer</c> I have to write some workaround like below
    /// <code>
    ///        var ruleParameter = new RuleInputModel()
    ///        {
    ///            Protocol = "tcp",
    ///            SourceIp = "2.2.2.2",
    ///            DestinationIp = "1.1.1.1",
    ///            SourcePort = "111",
    ///            DestinationPort = "222",
    ///            Jump = "DROP"
    ///        };
    ///
    ///        var parameter = new RulesCommandServiceAppendRequest()
    ///        {
    ///            Chain = Chain.INPUT
    ///        };
    ///
    ///        var writer = new WebSerializerWriter();
    ///        WebSerializer.ToQueryString(writer, parameter);
    ///        writer.AppendConcatenate();
    ///        writer.NamePrefix = $"{nameof(parameter.Data)}.";
    ///        WebSerializer.ToQueryString(writer, ruleParameter);
    ///        string request = $"{DEFAULT_PREFIX_URL}/{HOME_CONTROLLER_ROUTE_PREFIX}/Append?{writer.GetStringBuilder()}";
    ///        HttpResponseMessage response = await client.PostAsync(request, new WebSerializerFormUrlEncodedContent(writer));
    /// </code>
    /// </para>
    ///
    /// <para>
    /// This class based on https://stackoverflow.com/a/76874436/1539100
    /// </para>
    /// </summary>
    public static class Helpers
    {
        public static Dictionary<string, object> FlattenObject<T>(T source)
            where T : class, new()
        {
            return JObject.FromObject(source)
                .Descendants()
                .OfType<JValue>()
                .ToDictionary(jv => jv.Path, jv => jv.Value<object>());
        }

        public static string ToQueryString(this Dictionary<string, object> dict)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kvp in dict)
            {
                sb.Append("&")
                    .Append($"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value.ToString())}");
            }

            return sb.ToString()
                .Trim('&');
        }
    }
}
