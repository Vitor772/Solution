using Moq;
using Xunit;
using ServiceB.Services;
using FluentAssertions;

namespace ServiceTests.Services
{
    public class QueueConsumerTests
    {
        private readonly Mock<IPaymentProcessingService> _mockPaymentProcessingService;
        private readonly QueueConsumer _queueConsumer;

        public QueueConsumerTests()
        {
            _mockPaymentProcessingService = new Mock<IPaymentProcessingService>();
            _queueConsumer = new QueueConsumer(_mockPaymentProcessingService.Object);
        }

        [Fact]
        public async Task ProcessQueueMessage_ShouldProcessSuccessfully()
        {
            // Arrange
            var message = "Test Payment Intent";

            // Act
            var result = await _queueConsumer.ProcessQueueMessage(message);

            // Assert
            result.Should().Be("Processed");
        }
    }
}
