using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Moq;
using Xunit;

namespace Lykke.Service.ClientProfileRule.AzureRepositories.Tests
{
    public class RegulationRuleRepositoryTests : RepositoryTests
    {
        private readonly Mock<INoSQLTableStorage<RegulationRuleEntity>> _storageMock;
        private readonly RegulationRuleRepository _repository;

        public RegulationRuleRepositoryTests()
        {
            _storageMock = new Mock<INoSQLTableStorage<RegulationRuleEntity>>();
            _repository = new RegulationRuleRepository(_storageMock.Object);
        }

        [Fact]
        public async Task InsertAsync_OK()
        {
            // arrange
            const string regulationId = "regulationId";
            const string profileId = "profileId";

            var model = new RegulationRule
            {
                RegulationId = regulationId,
                ProfileId = profileId
            };

            RegulationRuleEntity entity = null;

            _storageMock.Setup(o => o.InsertAsync(It.IsAny<RegulationRuleEntity>(), It.IsAny<int[]>()))
                .Returns(Task.CompletedTask)
                .Callback((RegulationRuleEntity o, int[] p) => entity = o);

            // act
            await _repository.InsertAsync(model);

            // assert
            Assert.NotNull(entity);
            Assert.NotEmpty(entity.RowKey);
            Assert.NotEmpty(entity.PartitionKey);
            Assert.Equal(entity.RegulationId, regulationId);
            Assert.Equal(entity.ProfileId, profileId);
        }

        [Fact]
        public async Task GetAllAsync_OK()
        {
            // arrange
            var entity = new RegulationRuleEntity
            {
                RowKey = "key",
                PartitionKey = "part",
                ETag = "etag",
                Timestamp = new DateTimeOffset(),
                RegulationId = "reg1",
                ProfileId = "pr1"
            };

            _storageMock.Setup(o => o.GetDataAsync(It.IsAny<string>(), It.IsAny<Func<RegulationRuleEntity, bool>>()))
                .ReturnsAsync(new[] {entity});

            // act
            List<RegulationRule> model = (await _repository.GetAllAsync()).ToList();

            // assert
            Assert.NotNull(model);
            Assert.Single(model);
            Assert.Equal(entity.RegulationId, model[0].RegulationId);
            Assert.Equal(entity.ProfileId, model[0].ProfileId);
        }
    }
}
