using IPTables.Net;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Adapter;
using SystemInteract.Local;

namespace BSN.IpTables.Data.SystemTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ipTablesSystem = new IpTablesSystem(system: new LocalFactory(), tableAdapter: new IPTablesLibAdapter());
        }

        [Test]
        public void ListAllRules()
        {
            IpTablesChainSet rules = ipTablesSystem.GetRules(Chain.INPUT, IP_VERSION);
            Assert.Pass();
        }

        private IpTablesSystem ipTablesSystem;
        private const int IP_VERSION = 4;
        public struct Chain
        {
            public const string INPUT = "INPUT";
            public const string OUTPUT = "OUTPUT";
            public const string FORWARD = "FORWARD";
        }
    }
}