using Lykke.Service.ClientProfileRule.Client.Models;

namespace Lykke.Service.ClientProfileRule.Client
{
    internal static class AutorestClientMapper
    {
        public static RegulationRuleModel ToModel(this AutorestClient.Models.RegulationRuleModel model)
        {
            return new RegulationRuleModel
            {
                RegulationId = model.RegulationId,
                ProfileId = model.ProfileId
            };
        }
    }
}
