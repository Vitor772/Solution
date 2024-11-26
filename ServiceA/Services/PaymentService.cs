using ServiceA.Adapters;
using ServiceA.DTOs;

namespace ServiceA.Services
{
    public class PaymentService
    {
        private readonly PaymentQueueAdapter _queueAdapter;

        public PaymentService(PaymentQueueAdapter queueAdapter)
        {
            _queueAdapter = queueAdapter;
        }

        public async Task<string> CreatePaymentIntention(PaymentIntentionDto dto)
        {
            var uuid = Guid.NewGuid().ToString();
            var message = new PaymentQueueMessageDto
            {
                Uuid = uuid,
                Amount = dto.Amount,
                Currency = dto.Currency,
                CustomerId = dto.CustomerId
            };

            await _queueAdapter.SendMessageAsync(message);

            return uuid;
        }
    }
}
