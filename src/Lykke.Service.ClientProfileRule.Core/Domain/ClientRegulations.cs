using System.Collections.Generic;

namespace Lykke.Service.ClientProfileRule.Core.Domain
{
    public class ClientRegulations
    {
        public string ClientId { get; set; }

        public List<string> Regulations { get; set; }
    }
}
