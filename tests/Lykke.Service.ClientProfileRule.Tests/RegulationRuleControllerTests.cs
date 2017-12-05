using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Controllers;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.ClientProfileRule.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Lykke.Service.ClientProfileRule.Tests
{
    public class RegulationRuleControllerTests : ControllerTests
    {
        private readonly Mock<IRegulationRuleService> _regulationRuleServicMock;
        private readonly RegulationRuleController _controller;
        
        public RegulationRuleControllerTests()
        {
            _regulationRuleServicMock = new Mock<IRegulationRuleService>();

            _controller = new RegulationRuleController(_regulationRuleServicMock.Object);
        }

        [Fact]
        public async Task GetAll_Correct_Returned()
        {
            // arrange
            var regulationRule = new RegulationRule
            {
                RegulationId = "RegulationId",
                ProfileId = "ProfileId"
            };

            _regulationRuleServicMock.Setup(o => o.GetAllAsync())
                .ReturnsAsync(new[] {regulationRule});

            // act
            var result = await _controller.GetAll() as OkObjectResult;

            // assert
            Assert.IsAssignableFrom<IEnumerable<RegulationRuleModel>>(result.Value);
        }
    }
}
