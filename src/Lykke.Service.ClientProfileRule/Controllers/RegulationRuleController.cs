using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.ClientProfileRule.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.ClientProfileRule.Controllers
{
    [Route("api/[controller]")]
    public class RegulationRuleController : Controller
    {
        private readonly IRegulationRuleService _regulationRuleService;

        public RegulationRuleController(IRegulationRuleService regulationRuleService)
        {
            _regulationRuleService = regulationRuleService;
        }

        [HttpGet]
        [SwaggerOperation("RegulationRuleGetAll")]
        [ProducesResponseType(typeof(IEnumerable<RegulationRuleModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<RegulationRule> regulationRules = await _regulationRuleService.GetAllAsync();

            var model = Mapper.Map<IEnumerable<RegulationRuleModel>>(regulationRules);

            return Ok(model);
        }

        [HttpGet]
        [Route("{regulationId}")]
        [SwaggerOperation("RegulationRuleGetByRegulationId")]
        [ProducesResponseType(typeof(RegulationRuleModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByRegulationId(string regulationId)
        {
            RegulationRule regulationRule = await _regulationRuleService.GetByRegulationIdAsync(regulationId);

            var model = Mapper.Map<RegulationRuleModel>(regulationRule);

            return Ok(model);
        }

        [HttpPost]
        [SwaggerOperation("RegulationRuleAdd")]
        [ProducesResponseType(typeof(RegulationRuleModel), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Add([FromBody] RegulationRuleModel model)
        {
            RegulationRule regulationRule = Mapper.Map<RegulationRule>(model);

            await _regulationRuleService.AddAsync(regulationRule);

            return NoContent();
        }

        [HttpDelete]
        [Route("{regulationId}")]
        [SwaggerOperation("RegulationRuleDelete")]
        [ProducesResponseType(typeof(RegulationRuleModel), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string regulationId)
        {
            await _regulationRuleService.DeleteAsync(regulationId);

            return NoContent();
        }
    }
}
