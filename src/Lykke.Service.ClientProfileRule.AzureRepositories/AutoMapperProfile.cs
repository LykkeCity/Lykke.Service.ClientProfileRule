using AutoMapper;
using Lykke.Service.ClientProfileRule.Core.Domain;

namespace Lykke.Service.ClientProfileRule.AzureRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegulationRule, RegulationRuleEntity>(MemberList.Source)
                .IgnoreTableEntityFields()
                .ReverseMap();
        }

        public override string ProfileName => "EntityMapper";
    }
}
