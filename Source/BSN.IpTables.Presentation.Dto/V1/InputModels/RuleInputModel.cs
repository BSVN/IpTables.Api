using BSN.IpTables.Domain;
using Dynamitey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Presentation.Dto.V1.InputModels
{
    [DataContract]
    public class RuleInputModel
    {
        [DataMember(Order = 1)]
        public string? InterfaceName { get; set; }

        [DataMember(Order = 2)]
        public string? Protocol { get; set; }

        [DataMember(Order = 3)]
        public string? SourceIp { get; set; }

        [DataMember(Order = 4)]
        public string? DestinationIp { get; set; }

        [DataMember(Order = 5)]
        public string? SourcePort { get; set; }

        [DataMember(Order = 6)]
        public string? DestinationPort { get; set; }

        [DataMember(Order = 7)]
        public string? Jump { get; set; }

        public override string ToString()
        {
            var ruleBuilder = new IpTablesRuleBuilder();
            ruleBuilder.AddProtocol(Protocol)
                .AddSourceIp(SourceIp)
                .AddDestinationIp(DestinationIp)
                .AddSourcePort(SourcePort)
                .AddDestinationPort(DestinationPort)
                .AddJump(Jump);
            return ruleBuilder.ToString();
        }
    }
}
