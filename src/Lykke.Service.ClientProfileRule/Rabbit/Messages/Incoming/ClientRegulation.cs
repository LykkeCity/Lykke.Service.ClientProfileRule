namespace Lykke.Service.ClientProfileRule.Rabbit.Messages.Incoming
{
    public class ClientRegulation
    {
        public string RegulationId { get; set; }
        public bool Kyc { get; set; }
        public bool Active { get; set; }
    }
}
