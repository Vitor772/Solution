using Moq;
using Xunit;
using ServiceA.Services;
using FluentAssertions;
using ServiceA.DTOs;

namespace ServiceTests.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IQueueService> _mockQueueService;
        private readonly PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _mockQueueService = new Mock<IQueueService>();
            _paymentService = new PaymentService(_mockQueueService.Object);
        }

        [Fact]
        public async Task CreatePaymentIntent_ShouldReturnUuid_WhenValidData()
        {
            // Arrange
            var paymentDto = new PaymentIntentionDto { Amount = 100, Currency = "USD" };
            var uuid = "123e4567-e89b-12d3-a456-426614174000";
            _mockQueueService.Setup(q => q.SendMessageAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _paymentService.CreatePaymentIntent(paymentDto);

            // Assert
            result.Should().Be(uuid);
        }
    }
}
