using IPTables.Net.Iptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Domain
{
    public interface IIpTablesSystem
    {
        IpTablesChainSet List();
        void AppendRule(IpTablesRule rule);
        void CheckRule();
        void DeleteRule(IpTablesRule rule);
        void FlushRules();
        void InsertRule();
    }
}
