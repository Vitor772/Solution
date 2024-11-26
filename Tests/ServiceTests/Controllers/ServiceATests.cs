using ServiceA.Controllers;
using ServiceA.Services;    
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;


namespace ServiceTests.Controllers
{
    public class ServiceATests
    {
        private readonly ServiceAController _controller;
        private readonly Mock<IPaymentService> _mockPaymentService;

        public ServiceATests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _controller = new ServiceAController(_mockPaymentService.Object);
        }

        [Fact]
        public async Task CreatePaymentIntent_ShouldReturnOk_WhenValidData()
        {
            // Arrange
            var paymentDto = new PaymentIntentionDto { Amount = 100, Currency = "USD" };
            var uuid = "123e4567-e89b-12d3-a456-426614174000";
            _mockPaymentService.Setup(s => s.CreatePaymentIntent(paymentDto)).ReturnsAsync(uuid);

            // Act
            var result = await _controller.CreatePaymentIntent(paymentDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(uuid);
        }
    }
}
