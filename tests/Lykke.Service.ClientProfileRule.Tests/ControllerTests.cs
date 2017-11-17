using AutoMapper;

namespace Lykke.Service.ClientProfileRule.Tests
{
    public abstract class ControllerTests
    {
        private static readonly object Sync = new object();
        private static readonly bool Configured;

        static ControllerTests()
        {
            lock (Sync)
            {
                if (!Configured)
                {
                    Mapper.Initialize(m => m.AddProfile<AutoMapperProfile>());
                    Mapper.AssertConfigurationIsValid();
                }
                Configured = true;
            }
        }
    }
}
