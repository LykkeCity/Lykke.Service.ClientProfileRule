using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Client.Models;

namespace Lykke.Service.ClientProfileRule.Client
{
    public interface IClientProfileRuleClient
    {
        Task<IEnumerable<RegulationRuleModel>> GetAllAsync();
        Task<RegulationRuleModel> GetByRegulationIdAsync(string regulationId);
        Task GetByRegulationIdAsync(RegulationRuleModel regulationRule);
        Task DeleteAsync(string regulationIde);
    }
}
