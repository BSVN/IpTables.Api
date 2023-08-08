using AutoMapper;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
using IPTables.Net.Iptables;
using System;
using System.Linq;

namespace BSN.IpTables.Presentation.Dto.V1
{
    public class AppServiceViewMapperProfile : Profile
    {
        public AppServiceViewMapperProfile()
        {
            CreateMap<IpTablesChainSet, IpTablesChainSetViewModel>().ConvertUsing(new IpTablesChainSetViewModelConverter());
        }

        private class IpTablesChainSetViewModelConverter : ITypeConverter<IpTablesChainSet, IpTablesChainSetViewModel>
        {
            public IpTablesChainSetViewModel Convert(IpTablesChainSet source, IpTablesChainSetViewModel destination, ResolutionContext context)
            {
                return null;
            }
        }
    }
}

