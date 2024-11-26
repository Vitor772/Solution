using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ServiceA.Adapters;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ServiceTests
{
    public class ProgramTests
    {
        [Fact]
        public void ConfigureServices_ShouldAddAzureServiceBusAdapter()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "AzureServiceBus:ConnectionString", "fakeConnectionString" },
                        { "AzureServiceBus:QueueName", "payment-queue" }
                    })
                    .Build())
                .AddSingleton<AzureServiceBusAdapter>()
                .BuildServiceProvider();

            // Act
            var adapter = serviceProvider.GetService<AzureServiceBusAdapter>();

            // Assert
            Assert.NotNull(adapter);
        }
    }
}
