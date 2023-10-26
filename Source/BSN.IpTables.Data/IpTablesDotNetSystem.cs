using BSN.IpTables.Domain;
using IPTables.Net;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Adapter;
using IPTables.Net.Iptables.Adapter.Client;
using Microsoft.Extensions.Logging;
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
        public IpTablesDotNetSystem(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<IpTablesDotNetSystem>();
            logger.LogInformation("IpTablesDotNetSystem()");

            // TODO: What is difference between LibAdapter and BinaryAdapter
            ipTablesAdapter = new IPTablesBinaryAdapter();
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: ipTablesAdapter);
            inputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.INPUT, ipVersion: (int)IpVersion.V4);
            outputChain = ipTablesSystem.GetChain(table: Table.FILTER, chain: Chain.OUTPUT, ipVersion: (int)IpVersion.V4);
            table = ipTablesSystem.GetTableAdapter((int)IpVersion.V4);
        }

        public IpTablesChainSet List()
        {
            logger.LogInformation("IpTablesDotNetSystem:List()");
            IpTablesChainSet rules = ipTablesSystem.GetRules(Table.FILTER, IP_VERSION);
            return rules;
        }

        public void AppendRule(IpTablesRule rule)
        {
            logger.LogInformation("IpTablesDotNetSystem:AppendRule()");
            table.AddRule(rule);
        }

        public void CheckRule()
        {
            logger.LogInformation("IpTablesDotNetSystem:CheckRule()");
            throw new NotImplementedException();
        }

        public void DeleteRule(IpTablesRule rule)
        {
            logger.LogInformation("IpTablesDotNetSystem:DeleteRule()");
            table.DeleteRule(rule);
        }

        public void FlushRules()
        {
            logger.LogInformation("IpTablesDotNetSystem:FlushRules()");
            throw new NotImplementedException();
        }

        public void InsertRule()
        {
            logger.LogInformation("IpTablesDotNetSystem:InsertRule()");
            throw new NotImplementedException();
        }

        private readonly ILogger<IpTablesDotNetSystem> logger;
        private readonly IpTablesSystem ipTablesSystem;
        private readonly IpTablesChain inputChain;
        private readonly IpTablesChain outputChain;

        // FIXME: Dispose
        private readonly IIPTablesAdapterClient table;

        private readonly IIPTablesAdapter ipTablesAdapter;
        private const int IP_VERSION = 4;
    }
}
