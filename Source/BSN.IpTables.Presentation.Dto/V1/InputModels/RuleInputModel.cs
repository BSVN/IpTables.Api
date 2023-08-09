using Dynamitey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Presentation.Dto.V1.InputModels
{
    public class RuleInputModel
    {
        public string InterfaceName { get; set; }
        public string Protocol { get; set; }
        public string SourceIp { get; set; }
        public string DestinationIp { get; set; }
        public string SourcePort { get; set; }
        public string DestinationPort { get; set; }
        public string Chain { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (string.IsNullOrEmpty(InterfaceName))
                builder.Append($"i{InterfaceName}");
            return $"";
        }
    }
}
