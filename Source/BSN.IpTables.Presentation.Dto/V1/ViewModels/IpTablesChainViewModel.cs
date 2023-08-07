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
        public string ChainName { get; set; }
        public IpVersion IpVersion { get; set; }
        public ICollection<IpTablesRuleViewModel> Rules { get; set; }
    }
}
