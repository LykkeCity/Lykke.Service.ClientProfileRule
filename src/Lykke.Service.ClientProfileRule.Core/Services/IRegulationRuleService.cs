using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Core.Domain;

namespace Lykke.Service.ClientProfileRule.Core.Services
{
    public interface IRegulationRuleService
    {
        Task<IEnumerable<RegulationRule>> GetAllAsync();

        Task<RegulationRule> GetByRegulationIdAsync(string regulationId);

        Task AddAsync(RegulationRule regulationRule);

        Task DeleteAsync(string regulationId);

        Task UpplyAsync(ClientRegulations clientRegulations);
    }
}
