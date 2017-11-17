using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.ClientProfileRule.AzureRepositories
{
    public class RegulationRuleEntity : TableEntity
    {
        public string RegulationId { get; set; }

        public string ProfileId { get; set; }
    }
}
