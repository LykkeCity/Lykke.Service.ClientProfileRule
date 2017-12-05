using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Core.Domain;

namespace Lykke.Service.ClientProfileRule.Core.Repositories
{
    public interface IRegulationRuleRepository
    {
        Task InsertAsync(RegulationRule regulationRule);

        Task DeleteAsync(string regulationId);

        Task<IEnumerable<RegulationRule>> GetAllAsync();

        Task<RegulationRule> GetByRegulationIdAsync(string regulationId);
    }
}
