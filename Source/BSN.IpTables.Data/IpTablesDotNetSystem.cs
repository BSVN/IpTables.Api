using BSN.IpTables.Domain;
using IPTables.Net;
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
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: new IPTablesLibAdapter());
        }

        public void ListAllRules()
        {
            ipTablesSystem.GetRules("", IP_VERSION);
        }

        private readonly IpTablesSystem ipTablesSystem;
        private const int IP_VERSION = 4;
    }
}
