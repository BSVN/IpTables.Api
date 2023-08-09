using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Domain
{
    public class IpTablesRuleBuilder
    {

        public IpTablesRuleBuilder() :
            this(compactMode: false, strictMode: false)
        {
        }

        public IpTablesRuleBuilder(bool compactMode, bool strictMode)
        {
            stringBuilder = new StringBuilder();
            this.compactMode = compactMode;
            this.strictMode = strictMode;
        }

        /// <summary>
        /// Name of an interface via which a packet was received (only for packets entering the INPUT, FORWARD and PREROUTING chains).
        /// When the "!" argument is used before the interface name, the sense is inverted.
        /// If the interface name ends in a "+", then any interface which begins with this name will match.
        /// If this option is omitted, any interface name will match.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IpTablesRuleBuilder AddInboundInterface(string name, [CallerMemberName] string caller = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string parameter = compactMode ? "-i" : "--in-interface";
                stringBuilder.Append($"{parameter } {name}");
            }
            else if (strictMode)
            {
                throw new ArgumentNullException(caller);
            }

            return this;
        }

        /// <summary>
        /// Name of an interface via which a packet is going to be sent (for packets entering the FORWARD, OUTPUT and POSTROUTING chains).
        /// When the "!" argument is used before the interface name, the sense is inverted.
        /// If the interface name ends in a "+", then any interface which begins with this name will match.
        /// If this option is omitted, any interface name will match.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IpTablesRuleBuilder AddOutboundInterface(string name, [CallerMemberName] string caller = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string parameter = compactMode ? "-o" : "--out-interface";
                stringBuilder.Append($"{parameter } {name}");
            }
            else if (strictMode)
            {
                throw new ArgumentNullException(caller);
            }

            return this;
        }

        private readonly StringBuilder stringBuilder;
        private bool compactMode;
        private bool strictMode;
    }
}
