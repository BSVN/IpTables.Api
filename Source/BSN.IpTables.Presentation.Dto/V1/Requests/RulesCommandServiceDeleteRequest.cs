using BSN.IpTables.Presentation.Dto.V1.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Presentation.Dto.V1.Requests
{
    [DataContract]
    public class RulesCommandServiceDeleteRequest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [DataMember(Order = 1)]
        public string Chain { get; set; }

        [DataMember(Order = 2, Name = nameof(Rule))]
        public RuleInputModel Rule { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
