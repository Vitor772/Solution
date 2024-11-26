using Moq;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;
using Xunit;
using ServiceA.Adapters;

namespace ServiceTests
{
    public class AzureServiceBusAdapterTests
    {
        [Fact]
        public async Task SendMessageAsync_ShouldSendMessageToQueue()
        {
            // Arrange
            var mockClient = new Mock<ServiceBusClient>("fakeConnectionString");
            var mockSender = new Mock<ServiceBusSender>();

            mockClient.Setup(c => c.CreateSender(It.IsAny<string>())).Returns(mockSender.Object);

            var adapter = new AzureServiceBusAdapter("fakeConnectionString", "payment-queue");

            var message = "Test Payment Message";

            // Act
            await adapter.SendMessageAsync(message);

            // Assert
            mockSender.Verify(s => s.SendMessageAsync(It.Is<ServiceBusMessage>(m => m.Body.ToString() == message), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ReceiveMessageAsync_ShouldReceiveMessageFromQueue()
        {
            // Arrange
            var mockClient = new Mock<ServiceBusClient>("fakeConnectionString");
            var mockReceiver = new Mock<ServiceBusReceiver>();
            var mockMessage = new ServiceBusReceivedMessage("fakeMessageId", new BinaryData("Test Payment Message"));

            mockReceiver.Setup(r => r.ReceiveMessageAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockMessage);
            mockClient.Setup(c => c.CreateReceiver(It.IsAny<string>())).Returns(mockReceiver.Object);

            var adapter = new AzureServiceBusAdapter("fakeConnectionString", "payment-queue");

            // Act
            var receivedMessage = await adapter.ReceiveMessageAsync();

            // Assert
            Assert.Equal("Test Payment Message", receivedMessage.Body.ToString());
        }
    }
}
