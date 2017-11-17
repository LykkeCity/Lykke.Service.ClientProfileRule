using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.ClientProfileRule.Core.Services;

namespace Lykke.Service.ClientProfileRule.Clients
{
    public class KycService : IKycService
    {
        public Task EnsureClientProfile(string clientId, string profileId)
        {
            return Task.CompletedTask;
        }
    }
}
