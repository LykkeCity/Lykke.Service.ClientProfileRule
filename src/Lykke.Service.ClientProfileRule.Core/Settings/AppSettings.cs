using Lykke.Service.ClientProfileRule.Core.Settings.ServiceSettings;
using Lykke.Service.ClientProfileRule.Core.Settings.SlackNotifications;

namespace Lykke.Service.ClientProfileRule.Core.Settings
{
    public class AppSettings
    {
        public ClientProfileRuleSettings ClientProfileRuleService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
