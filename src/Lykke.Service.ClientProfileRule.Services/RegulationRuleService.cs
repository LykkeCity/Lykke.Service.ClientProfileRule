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

        public Task<IEnumerable<RegulationRule>> GetAllAsync()
        {
            return _regulationRuleRepository.GetAllAsync();
        }
        public Task<RegulationRule> GetByRegulationIdAsync(string regulationId)
        {
            return _regulationRuleRepository.GetByRegulationIdAsync(regulationId);
        }

        public Task AddAsync(RegulationRule regulationRule)
        {
            return _regulationRuleRepository.InsertAsync(regulationRule);
        }

        public Task DeleteAsync(string regulationId)
        {
            return _regulationRuleRepository.DeleteAsync(regulationId);
        }

        public async Task UpplyAsync(ClientRegulations clientRegulations)
        {
            foreach (string regulationId in clientRegulations.Regulations)
            {
                RegulationRule regulationRule =
                    await _regulationRuleRepository.GetByRegulationIdAsync(regulationId);

                if (regulationRule == null)
                {
                    throw new RegulationRuleNotFoundException
                    {
                        RegulationId = regulationId
                    };
                }

                await _kycService.EnsureClientProfile(clientRegulations.ClientId, regulationRule.ProfileId);
            }
        }
    }
}
