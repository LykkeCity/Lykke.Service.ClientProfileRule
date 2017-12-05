using Lykke.Service.ClientProfileRule.Settings.ServiceSettings;
using Lykke.Service.ClientProfileRule.Settings.SlackNotifications;
using Lykke.Service.Kyc.Client;

namespace Lykke.Service.ClientProfileRule.Settings
{
    public class AppSettings
    {
        public ClientProfileRuleSettings ClientProfileRuleService { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        public KycServiceSettings KycServiceClient { get; set; }
    }
}
