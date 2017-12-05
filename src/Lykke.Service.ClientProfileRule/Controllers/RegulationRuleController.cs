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

        /// <summary>
        /// Returns all regulation rules.
        /// </summary>
        /// <response code="200">The collection of regulation rules.</response>
        [HttpGet]
        [SwaggerOperation("RegulationRuleGetAll")]
        [ProducesResponseType(typeof(IEnumerable<RegulationRuleModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<RegulationRule> regulationRules = await _regulationRuleService.GetAllAsync();

            var model = Mapper.Map<IEnumerable<RegulationRuleModel>>(regulationRules);

            return Ok(model);
        }

        /// <summary>
        /// Returns a regulation rule by specified regulation id.
        /// </summary>
        /// <param name="regulationId">The regulation id.</param>
        /// <response code="200">The regulation rule.</response>
        /// <response code="404">Regulation rule not found exist.</response>
        [HttpGet]
        [Route("{regulationId}")]
        [SwaggerOperation("RegulationRuleGetByRegulationId")]
        [ProducesResponseType(typeof(RegulationRuleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByRegulationId(string regulationId)
        {
            RegulationRule regulationRule = await _regulationRuleService.GetByRegulationIdAsync(regulationId);

            var model = Mapper.Map<RegulationRuleModel>(regulationRule);

            return Ok(model);
        }

        /// <summary>
        /// Adds regulation rules.
        /// </summary>
        /// <response code="204">Regulation successfully added.</response>
        /// <response code="400">Invalid model what describe a regulation.</response>
        /// <response code="409">Regulation rule already exist.</response>
        [HttpPost]
        [SwaggerOperation("RegulationRuleAdd")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add([FromBody] NewRegulationRuleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponse.Create("Invalid model.", ModelState));
            }

            RegulationRule regulationRule = Mapper.Map<RegulationRule>(model);

            await _regulationRuleService.AddAsync(regulationRule);

            return NoContent();
        }

        /// <summary>
        /// Deletes the regulation rule by specified id.
        /// </summary>
        /// <param name="regulationId">The regulation id.</param>
        /// <response code="204">Regulation rule successfully deleted.</response>
        [HttpDelete]
        [Route("{regulationId}")]
        [SwaggerOperation("RegulationRuleDelete")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string regulationId)
        {
            await _regulationRuleService.DeleteAsync(regulationId);

            return NoContent();
        }
    }
}
