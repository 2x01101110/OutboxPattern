using MassTransit;
using OutboxPattern.Shared.Outbox;
using System.Text.Json;

namespace OutboxPattern.Demo.Services.API.Application.Consumers
{
    public class OrderCreatedConsumer :
        IConsumer<OrderCreated>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(
            ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            _logger.LogInformation(JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { WriteIndented = true }));

            return Task.CompletedTask;
        }
    }
}
