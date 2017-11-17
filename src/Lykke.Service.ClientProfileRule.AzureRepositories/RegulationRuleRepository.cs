using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Repositories;

namespace Lykke.Service.ClientProfileRule.AzureRepositories
{
    public class RegulationRuleRepository : IRegulationRuleRepository
    {
        private readonly INoSQLTableStorage<RegulationRuleEntity> _storage;

        public RegulationRuleRepository(INoSQLTableStorage<RegulationRuleEntity> storage)
        {
            _storage = storage;
        }

        public Task InsertAsync(RegulationRule regulationRule)
        {
            RegulationRuleEntity entity = CreateEntity(regulationRule.RegulationId);

            Mapper.Map(regulationRule, entity);

            return _storage.InsertAsync(entity);
        }

        public Task DeleteAsync(string regulationId)
        {
            return _storage.DeleteAsync(GetPartitionKey(), GetRowKey(regulationId));
        }

        public async Task<IEnumerable<RegulationRule>> GetAllAsync()
        {
            IEnumerable<RegulationRuleEntity> entities = await _storage.GetDataAsync(GetPartitionKey());

            return Mapper.Map<IEnumerable<RegulationRule>>(entities);
        }

        public async Task<RegulationRule> GetByRegulationIdAsync(string regulationId)
        {
            RegulationRuleEntity entity = await _storage.GetDataAsync(GetPartitionKey(), GetRowKey(regulationId));

            return Mapper.Map<RegulationRule>(entity);
        }

        private static RegulationRuleEntity CreateEntity(string regulationId)
            => new RegulationRuleEntity
            {
                PartitionKey = GetPartitionKey(),
                RowKey = GetRowKey(regulationId)
            };

        private static string GetPartitionKey()
            => "RegulationRule";

        private static string GetRowKey(string regulationId)
            => $"{regulationId}";
    }
}
