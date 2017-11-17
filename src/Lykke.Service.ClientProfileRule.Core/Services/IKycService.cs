using System.Threading.Tasks;

namespace Lykke.Service.ClientProfileRule.Core.Services
{
    public interface IKycService
    {
        Task EnsureClientProfile(string clientId, string profileId);
    }
}
