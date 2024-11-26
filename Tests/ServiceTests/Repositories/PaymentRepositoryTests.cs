using Moq;
using Xunit;
using ServiceB.Repositories;
using FluentAssertions;
using MongoDB.Driver;
using ServiceB.Models;

namespace ServiceTests.Repositories
{
    public class PaymentRepositoryTests
    {
        private readonly Mock<IMongoCollection<Payment>> _mockCollection;
        private readonly PaymentRepository _repository;

        public PaymentRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<Payment>>();
            _repository = new PaymentRepository(_mockCollection.Object);
        }

        [Fact]
        public async Task SavePayment_ShouldInsertPayment()
        {
            // Arrange
            var payment = new Payment { Id = "123e4567-e89b-12d3-a456-426614174000", Amount = 100 };

            // Act
            await _repository.SavePayment(payment);

            // Assert
            _mockCollection.Verify(m => m.InsertOneAsync(It.IsAny<Payment>(), null, default), Times.Once);
        }
    }
}
