using System.Threading.Tasks;

namespace Lykke.Service.ClientProfileRule.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}