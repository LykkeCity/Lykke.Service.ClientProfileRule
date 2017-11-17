using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Repositories;
using Lykke.Service.ClientProfileRule.Core.Services;
using Moq;
using Xunit;

namespace Lykke.Service.ClientProfileRule.Services.Tests
{
    public class RegulationRuleServiceTests
    {
        private readonly Mock<IRegulationRuleRepository> _regulationRuleRepositoryMock;
        private readonly Mock<IKycService> _kycServiceMock;
        private readonly RegulationRuleService _service;

        public RegulationRuleServiceTests()
        {
            _regulationRuleRepositoryMock = new Mock<IRegulationRuleRepository>();
            _kycServiceMock = new Mock<IKycService>();

            _service = new RegulationRuleService(_regulationRuleRepositoryMock.Object, _kycServiceMock.Object);
        }

        [Fact]
        public async Task UpplyAsync_OK()
        {
            // arrange
            string profileId = null;

            var clientRegulations = new ClientRegulations
            {
                ClientId = "me",
                Regulations = new List<string> { "r1" }
            };

            var regulationRule = new RegulationRule
            {
                RegulationId = "r1",
                ProfileId = "p1"
            };

            _regulationRuleRepositoryMock.Setup(o => o.GetByRegulationIdAsync(It.IsAny<string>()))
                .ReturnsAsync(regulationRule);

            _kycServiceMock.Setup(o => o.EnsureClientProfile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Callback((string a, string b) => profileId = b);

            // act
            await _service.UpplyAsync(clientRegulations);

            // assert
            Assert.Equal(profileId, regulationRule.ProfileId);
        }
    }
}
