using AutoMapper;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
using IPTables.Net.Iptables;
using IPTables.Net.Iptables.Modules;
using IPTables.Net.Iptables.Modules.Core;
using System;
using System.Linq;

namespace BSN.IpTables.Presentation.Dto.V1
{
    public class AppServiceViewMapperProfile : Profile
    {
        public AppServiceViewMapperProfile()
        {
            CreateMap<IpTablesRule, IpTablesRuleViewModel>().ConvertUsing(new IpTablesRuleViewModelConverter());
            CreateMap<IpTablesChain, IpTablesChainViewModel>().ConvertUsing(new IpTablesChainViewModelConverter());
            CreateMap<IpTablesChainSet, IpTablesChainSetViewModel>().ConvertUsing(new IpTablesChainSetViewModelConverter());
        }

        private class IpTablesChainSetViewModelConverter : ITypeConverter<IpTablesChainSet, IpTablesChainSetViewModel>
        {
            public IpTablesChainSetViewModel Convert(IpTablesChainSet source, IpTablesChainSetViewModel destination, ResolutionContext context)
            {
                return new IpTablesChainSetViewModel()
                {
                    IpTablesChains = context.Mapper.Map<IEnumerable<IpTablesChainViewModel>>(source.Chains)
                };
            }
        }

        private class IpTablesChainViewModelConverter : ITypeConverter<IpTablesChain, IpTablesChainViewModel>
        {
            public IpTablesChainViewModel Convert(IpTablesChain source, IpTablesChainViewModel destination, ResolutionContext context)
            {
                return new IpTablesChainViewModel()
                {
                    Name = source.Name,
                    TableName = source.Table,
                    IpVersion = (Domain.IpVersion)source.IpVersion,
                    Rules = context.Mapper.Map<IEnumerable<IpTablesRuleViewModel>>(source.Rules)
                };
            }
        }

        private class IpTablesRuleViewModelConverter : ITypeConverter<IpTablesRule, IpTablesRuleViewModel>
        {
            public IpTablesRuleViewModel Convert(IpTablesRule source, IpTablesRuleViewModel destination, ResolutionContext context)
            {
                // TODO: Check if does not CoreModule
                var detailedSource = (source.ModuleData.FirstOrDefault() as CoreModule) ?? throw new NullReferenceException("Source must be not null");
                return new IpTablesRuleViewModel()
                {
                    SourceIp = detailedSource.Source.Null ? string.Empty : detailedSource.Source.Value.Address.ToString(),
                    DestinationIp = detailedSource.Destination.Null ? string.Empty : detailedSource.Destination.Value.Address.ToString(),
                    Protocol = detailedSource.Protocol.Null ? string.Empty : detailedSource.Protocol.Value.ToString(),
                    Target = detailedSource.Target.ToString(),
                    InterfaceName = (detailedSource.InInterface.Value ?? detailedSource.OutInterface.Value)?.ToString() ?? string.Empty,
                };
            }
        }
    }
}

