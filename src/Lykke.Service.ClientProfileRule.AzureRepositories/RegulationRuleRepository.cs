using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Exceptions;
using Lykke.Service.ClientProfileRule.Core.Repositories;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.Protocol;

namespace Lykke.Service.ClientProfileRule.AzureRepositories
{
    public class RegulationRuleRepository : IRegulationRuleRepository
    {
        private readonly INoSQLTableStorage<RegulationRuleEntity> _storage;

        public RegulationRuleRepository(INoSQLTableStorage<RegulationRuleEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IEnumerable<RegulationRule>> GetAllAsync()
        {
            IEnumerable<RegulationRuleEntity> entities = await _storage.GetDataAsync(GetPartitionKey());

            return Mapper.Map<IEnumerable<RegulationRule>>(entities);
        }

        public async Task InsertAsync(RegulationRule regulationRule)
        {
            RegulationRuleEntity entity = CreateEntity(regulationRule.RegulationId);

            Mapper.Map(regulationRule, entity);

            try
            {
                await _storage.InsertAsync(entity);
            }
            catch (StorageException exception)
            {
                if (exception.RequestInformation != null &&
                    exception.RequestInformation.HttpStatusCode == 409 &&
                    exception.RequestInformation.ExtendedErrorInformation.ErrorCode == TableErrorCodeStrings.EntityAlreadyExists)
                {
                    throw new RegulationRuleAlreadyExistsException("Already exists", exception)
                    {
                        RegulationId = regulationRule.RegulationId
                    };
                }
            }
        }

        public async Task DeleteAsync(string regulationId)
        {
            if (string.IsNullOrEmpty(regulationId))
                throw new ArgumentNullException(nameof(regulationId));

            await _storage.DeleteAsync(GetPartitionKey(), GetRowKey(regulationId));
        }

        public async Task<RegulationRule> GetByRegulationIdAsync(string regulationId)
        {
            if(string.IsNullOrEmpty(regulationId))
                throw new ArgumentNullException(nameof(regulationId));

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
            => $"{regulationId}".Trim().ToLower();
    }
}
