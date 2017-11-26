using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.Kyc.Client;
using Lykke.SettingsReader;

namespace Lykke.Service.ClientProfileRule.Clients
{
    public class KycService : IKycService
    {
        private readonly IReloadingManager<KycServiceSettings> _settings;
        private readonly ILog _log;

        public KycService(IReloadingManager<KycServiceSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        public async Task EnsureClientProfile(string clientId, string profileId)
        {
            var client = new ProfileServiceV2Client(_settings.CurrentValue, _log);

            await client.GetProfie(clientId, profileId);
        }
    }
}
