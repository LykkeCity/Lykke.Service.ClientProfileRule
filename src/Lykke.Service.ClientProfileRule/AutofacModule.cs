using Autofac;
using Common;
using Common.Log;
using Lykke.Service.ClientProfileRule.Clients;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.ClientProfileRule.Rabbit.Subscribers;
using Lykke.Service.ClientProfileRule.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.ClientProfileRule
{
    public class AutofacModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;
        private readonly ILog _log;

        public AutofacModule(IReloadingManager<AppSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new KycService(_settings.Nested(o => o.KycServiceClient), _log))
                .As<IKycService>();

            builder.RegisterType<RegulationSubscriber>()
                .AsSelf()
                .As<IStartable>()
                .As<IStopable>()
                .AutoActivate()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.ClientProfileRuleService.RabbitMq
                    .RegulationQueue));
        }
    }
}
