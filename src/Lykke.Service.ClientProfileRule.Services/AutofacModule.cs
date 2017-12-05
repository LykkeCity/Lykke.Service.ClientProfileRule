using Autofac;
using Lykke.Service.ClientProfileRule.Core.Services;

namespace Lykke.Service.ClientProfileRule.Services
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterType<RegulationRuleService>()
                .As<IRegulationRuleService>();
        }
    }
}
