using System;
using Common.Log;

namespace Lykke.Service.ClientProfileRule.Client
{
    public class ClientProfileRuleClient : IClientProfileRuleClient, IDisposable
    {
        private readonly ILog _log;

        public ClientProfileRuleClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
