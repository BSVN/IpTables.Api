using BSN.IpTables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Presentation.Dto.V1.ViewModels
{
    public class IpTablesChainViewModel
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public IpVersion IpVersion { get; set; }
        public IEnumerable<IpTablesRuleViewModel> Rules { get; set; }
    }
}
