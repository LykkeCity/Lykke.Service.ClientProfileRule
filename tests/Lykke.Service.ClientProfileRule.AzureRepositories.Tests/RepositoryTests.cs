using AutoMapper;

namespace Lykke.Service.ClientProfileRule.AzureRepositories.Tests
{
    public abstract class RepositoryTests
    {
        private static readonly object Sync = new object();
        private static readonly bool Configured;

        static RepositoryTests()
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
