using ServiceB.DTOs;
using ServiceB.Models;
using ServiceB.Repositories;

namespace ServiceB.Services
{
    public class PaymentService
    {
        private readonly PaymentRepository _repository;

        public PaymentService(PaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task ProcessPaymentAsync(PaymentQueueMessageDto message)
        {
            var payment = new PaymentIntention
            {
                Uuid = message.Uuid,
                Amount = message.Amount,
                Currency = message.Currency,
                CustomerId = message.CustomerId,
                Status = new Random().Next(0, 2) == 0 ? "Success" : "Failed"
            };

            if (payment.Status == "Success")
            {
                await _repository.InsertAsync(payment);
            }
            else
            {
                throw new Exception("Erro simulado no processamento.");
            }
        }
    }
}
