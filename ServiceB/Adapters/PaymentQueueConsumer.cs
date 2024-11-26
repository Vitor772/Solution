using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceB.DTOs;
using ServiceB.Services;

namespace ServiceB.Adapters
{
    public class PaymentQueueConsumer
    {
        private readonly ServiceBusProcessor _processor;

        public PaymentQueueConsumer(string connectionString, string queueName, PaymentService paymentService)
        {
            var client = new ServiceBusClient(connectionString);
            _processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            _processor.ProcessMessageAsync += async args =>
            {
                try
                {
                    var messageBody = args.Message.Body.ToString();
                    var paymentMessage = JsonConvert.DeserializeObject<PaymentQueueMessageDto>(messageBody);
                    await paymentService.ProcessPaymentAsync(paymentMessage);
                    await args.CompleteMessageAsync(args.Message);
                }
                catch
                {
                    // Retenta automaticamente
                }
            };

            _processor.ProcessErrorAsync += args =>
            {
                Console.WriteLine($"Erro: {args.Exception.Message}");
                return Task.CompletedTask;
            };
        }

        public async Task StartAsync() => await _processor.StartProcessingAsync();
        public async Task StopAsync() => await _processor.StopProcessingAsync();
    }
}
