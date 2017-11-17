using System.Linq;
using AutoMapper;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Models;
using Lykke.Service.ClientProfileRule.Rabbit.Messages.Incoming;

namespace Lykke.Service.ClientProfileRule
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegulationRule, RegulationRuleModel>(MemberList.Source)
                .ReverseMap();

            CreateMap<ClientRegulationsMessage, ClientRegulations>(MemberList.Destination)
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Regulations,
                    opt => opt.MapFrom(src => src.Regulations.Select(o => o.RegulationId)));
        }
    }
}
