namespace ServiceA.DTOs
{
    public class PaymentQueueMessageDto
    {
        public string Uuid { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
    }
}
