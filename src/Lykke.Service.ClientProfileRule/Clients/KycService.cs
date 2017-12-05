using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.Kyc.Client;
using Lykke.SettingsReader;

namespace Lykke.Service.ClientProfileRule.Clients
{
    public class KycService : IKycService
    {
        private readonly ProfileServiceV2Client _client;

        public KycService(IReloadingManager<KycServiceSettings> settings, ILog log)
        {
            _client = new ProfileServiceV2Client(settings.CurrentValue, log);
        }

        public async Task EnsureClientProfile(string clientId, string profileId)
        {
            await _client.GetProfie(clientId, profileId);
        }
    }
}
