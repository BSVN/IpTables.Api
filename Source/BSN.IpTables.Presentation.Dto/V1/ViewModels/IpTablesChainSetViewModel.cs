﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSN.IpTables.Presentation.Dto.V1.ViewModels
{
    public class IpTablesChainSetViewModel
    {
        public ICollection<IpTablesChainViewModel> IpTablesChains { get; set; }
    }
}