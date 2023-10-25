using BSN.IpTables.Domain;
using IPTables.Net;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Adapter;
using IPTables.Net.Iptables.Adapter.Client;
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
            Console.WriteLine("IpTablesDotNetSystem()");

            // TODO: What is difference between LibAdapter and BinaryAdapter
            ipTablesAdapter = new IPTablesBinaryAdapter();
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: ipTablesAdapter);
            inputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.INPUT, ipVersion: (int)IpVersion.V4);
            outputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.OUTPUT, ipVersion: (int)IpVersion.V4);
            table = ipTablesSystem.GetTableAdapter((int)IpVersion.V4);
            Console.WriteLine("IpTablesDotNetSystem() end");
        }

        public IpTablesChainSet List()
        {
            Console.WriteLine("IpTablesDotNetSystem:List()");
            IpTablesChainSet rules = ipTablesSystem.GetRules(Table.FILTER, IP_VERSION);
            Console.WriteLine("IpTablesDotNetSystem:List() end");
            return rules;
        }

        public void AppendRule(IpTablesRule rule)
        {
            table.AddRule(rule);
        }

        public void CheckRule()
        {
            throw new NotImplementedException();
        }

        public void DeleteRule(IpTablesRule rule)
        {
            table.DeleteRule(rule);
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

        // FIXME: Dispose
        private readonly IIPTablesAdapterClient table;

        private readonly IIPTablesAdapter ipTablesAdapter;
        private const int IP_VERSION = 4;
    }
}
