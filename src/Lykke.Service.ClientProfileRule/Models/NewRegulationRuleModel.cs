using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.ClientProfileRule.Models
{
    public class NewRegulationRuleModel
    {
        [Required]
        public string RegulationId { get; set; }

        [Required]
        public string ProfileId { get; set; }
    }
}
