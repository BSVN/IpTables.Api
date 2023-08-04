using BSN.IpTables.Domain;
using IPTables.Net;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Adapter;
using SystemInteract.Local;

namespace BSN.IpTables.Data.SystemTest
{
    public class IpTablesDotNetSystemTest
    {
        [SetUp]
        public void Initialize()
        {
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: new IPTablesBinaryAdapter());
        }

        [Test]
        public void ListAllRules()
        {
            IpTablesChainSet rules = ipTablesSystem.GetRules(Table.FILTER, IP_VERSION);
            Assert.Pass();
        }

        private IpTablesSystem ipTablesSystem;
        private const int IP_VERSION = 4;

    }
}