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
        }

        public IpTablesChainSet List()
        {
            IpTablesChainSet rules = ipTablesSystem.GetRules(Table.FILTER, IP_VERSION);
            return rules;
        }

        public void AppendRule(IpTablesRule rule)
        {
            throw new NotImplementedException();
        }

        public void CheckRule()
        {
            throw new NotImplementedException();
        }

        public void DeleteRule()
        {
            throw new NotImplementedException();
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
        private const int IP_VERSION = 4;
    }
}
