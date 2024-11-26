using Moq;
using Xunit;
using ServiceA.Services;
using FluentAssertions;
using Azure.Messaging.ServiceBus;

namespace ServiceTests.Services
{
    public class QueueServiceTests
    {
        private readonly Mock<IAzureServiceBusAdapter> _mockServiceBusAdapter;
        private readonly QueueService _queueService;

        public QueueServiceTests()
        {
            _mockServiceBusAdapter = new Mock<IAzureServiceBusAdapter>();
            _queueService = new QueueService(_mockServiceBusAdapter.Object);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldCallServiceBusAdapter()
        {
            // Arrange
            var message = "Test Payment Intent";

            // Act
            await _queueService.SendMessageAsync(message);

            // Assert
            _mockServiceBusAdapter.Verify(m => m.SendMessageAsync(It.Is<string>(s => s == message)), Times.Once);
        }
    }
}
