using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceA.DTOs;

namespace ServiceA.Adapters
{
    public class PaymentQueueAdapter
    {
        private readonly ServiceBusSender _sender;

        public PaymentQueueAdapter(string connectionString, string queueName)
        {
            var client = new ServiceBusClient(connectionString);
            _sender = client.CreateSender(queueName);
        }

        public async Task SendMessageAsync(PaymentQueueMessageDto message)
        {
            var serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(message));
            await _sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
