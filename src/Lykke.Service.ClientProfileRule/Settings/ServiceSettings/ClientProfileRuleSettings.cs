using Lykke.Service.ClientProfileRule.Settings.ServiceSettings.Db;
using Lykke.Service.ClientProfileRule.Settings.ServiceSettings.Rabbit;

namespace Lykke.Service.ClientProfileRule.Settings.ServiceSettings
{
    public class ClientProfileRuleSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }
    }
}
