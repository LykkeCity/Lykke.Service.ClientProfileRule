using System.Collections.Generic;

namespace Lykke.Service.ClientProfileRule.Rabbit.Messages.Incoming
{
    public class ClientRegulationsMessage
    {
        public ClientRegulationsMessage()
        {
            Regulations = new List<ClientRegulation>();
        }

        public string ClientId { get; set; }
        public List<ClientRegulation> Regulations { get; set; }
    }
}
