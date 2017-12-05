using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Exceptions;
using Lykke.Service.ClientProfileRule.Core.Repositories;
using Lykke.Service.ClientProfileRule.Core.Services;

namespace Lykke.Service.ClientProfileRule.Services
{
    public class RegulationRuleService : IRegulationRuleService
    {
        private readonly IRegulationRuleRepository _regulationRuleRepository;
        private readonly IKycService _kycService;

        public RegulationRuleService(IRegulationRuleRepository regulationRuleRepository, IKycService kycService)
        {
            _regulationRuleRepository = regulationRuleRepository;
            _kycService = kycService;
        }

        public async Task<IEnumerable<RegulationRule>> GetAllAsync()
        {
            return await _regulationRuleRepository.GetAllAsync();
        }
        public async Task<RegulationRule> GetByRegulationIdAsync(string regulationId)
        {
            RegulationRule regulationRule = await _regulationRuleRepository.GetByRegulationIdAsync(regulationId);

            if (regulationRule == null)
            {
                throw new RegulationRuleNotFoundException("Regulation rule not found.")
                {
                    RegulationId = regulationId
                };
            }

            return regulationRule;
        }

        public async Task AddAsync(RegulationRule regulationRule)
        {
            await _regulationRuleRepository.InsertAsync(regulationRule);
        }

        public async Task DeleteAsync(string regulationId)
        {
            await _regulationRuleRepository.DeleteAsync(regulationId);
        }

        public async Task UpplyAsync(ClientRegulations clientRegulations)
        {
            foreach (string regulationId in clientRegulations.Regulations)
            {
                RegulationRule regulationRule =
                    await _regulationRuleRepository.GetByRegulationIdAsync(regulationId);

                if (regulationRule == null)
                {
                    throw new RegulationRuleNotFoundException("Regulation rule not found.")
                    {
                        RegulationId = regulationId
                    };
                }

                await _kycService.EnsureClientProfile(clientRegulations.ClientId, regulationRule.ProfileId);
            }
        }
    }
}
