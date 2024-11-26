using Moq;
using Xunit;
using ServiceB.Controllers;
using ServiceB.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;

namespace ServiceTests.Controllers
{
    public class ServiceBTests
    {
        private readonly ServiceBController _controller;
        private readonly Mock<IPaymentProcessingService> _mockPaymentProcessingService;

        public ServiceBTests()
        {
            _mockPaymentProcessingService = new Mock<IPaymentProcessingService>();
            _controller = new ServiceBController(_mockPaymentProcessingService.Object);
        }

        [Fact]
        public async Task GetPaymentStatus_ShouldReturnOk_WhenPaymentExists()
        {
            // Arrange
            var uuid = "123e4567-e89b-12d3-a456-426614174000";
            var status = "Success";
            _mockPaymentProcessingService.Setup(s => s.GetPaymentStatus(uuid)).ReturnsAsync(status);

            // Act
            var result = await _controller.GetPaymentStatus(uuid);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(status);
        }
    }
}
