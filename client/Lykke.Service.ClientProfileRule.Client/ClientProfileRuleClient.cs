using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Client.AutorestClient;
using Lykke.Service.ClientProfileRule.Client.Models;

namespace Lykke.Service.ClientProfileRule.Client
{
    public class ClientProfileRuleClient : IClientProfileRuleClient, IDisposable
    {
        private ClientProfileRuleAPI _service;

        public ClientProfileRuleClient(string serviceUrl)
        {
            _service = new ClientProfileRuleAPI(new Uri(serviceUrl));
        }

        public async Task<IEnumerable<RegulationRuleModel>> GetAllAsync()
        {
            return (await _service.RegulationRuleGetAllAsync()).Select(o => o.ToModel());
        }

        public async Task<RegulationRuleModel> GetByRegulationIdAsync(string regulationId)
        {
            return (await _service.RegulationRuleGetByRegulationIdAsync(regulationId)).ToModel();
        }

        public Task GetByRegulationIdAsync(RegulationRuleModel regulationRule)
        {
            var model = new AutorestClient.Models.RegulationRuleModel
            {
                RegulationId = regulationRule.RegulationId,
                ProfileId = regulationRule.ProfileId
            };

            return _service.RegulationRuleAddAsync(model);
        }

        public Task DeleteAsync(string regulationIde)
        {
            return _service.RegulationRuleDeleteAsync(regulationIde);
        }

        public void Dispose()
        {
            if (_service == null)
                return;

            _service.Dispose();
            _service = null;
        }
    }
}
