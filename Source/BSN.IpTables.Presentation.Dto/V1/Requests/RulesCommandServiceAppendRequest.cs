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
    public class RulesCommandServiceAppendRequest
    {
        [DataMember(Order = 1)]
        public string Chain { get; set; }

        [DataMember(Order = 2, Name = nameof(Data))]
        public RuleInputModel Data { get; set; }
    }
}
