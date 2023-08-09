using BSN.IpTables.Domain;
using IPTables.Net;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInteract.Local;

namespace BSN.IpTables.Data
{
    public class IpTablesDotNetSystem : IIpTablesSystem
    {
        public IpTablesDotNetSystem()
        {
            // TODO: What is difference between LibAdapter and BinaryAdapter
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: new IPTablesBinaryAdapter());
            inputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.INPUT, ipVersion: (int)IpVersion.V4);
            outputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.OUTPUT, ipVersion: (int)IpVersion.V4);
        }

        public IpTablesChainSet List()
        {
            IpTablesChainSet rules = ipTablesSystem.GetRules(Table.FILTER, IP_VERSION);
            return rules;
        }

        public void AppendRule(IpTablesRule rule)
        {
            inputChain.AddRule(rule);
        }

        public void CheckRule()
        {
            throw new NotImplementedException();
        }

        public void DeleteRule(IpTablesRule rule)
        {
            inputChain.DeleteRule(rule);
        }

        public void FlushRules()
        {
            throw new NotImplementedException();
        }

        public void InsertRule()
        {
            throw new NotImplementedException();
        }

        private readonly IpTablesSystem ipTablesSystem;
        private readonly IpTablesChain inputChain;
        private readonly IpTablesChain outputChain;
        private const int IP_VERSION = 4;
    }
}
