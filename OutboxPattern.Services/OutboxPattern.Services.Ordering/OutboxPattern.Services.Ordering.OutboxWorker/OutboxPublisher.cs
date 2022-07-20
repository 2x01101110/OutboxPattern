using MassTransit;
using OutboxPattern.Shared.BuildingBlocks.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboxPattern.Services.Ordering.OutboxWorker
{
    internal class OutboxPublisher : BackgroundService
    {
        //private readonly IPublishEndpoint _publishEndpoint;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxPublisher> _logger;
        private readonly IBus _bus;

        public OutboxPublisher(
            IServiceProvider serviceProvider,
            ILogger<OutboxPublisher> logger,
            IBus bus)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
            this._bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using OutboxDbContext? outboxDbContext = this._serviceProvider
                    .CreateScope().ServiceProvider
                    .GetService<OutboxDbContext>();

                if (outboxDbContext == null) throw new Exception("OutboxDbContext is null.");

                var outboxMessages = outboxDbContext.Outbox
                    .Where(x => x.Status == Status.Created)
                    .ToList();

                foreach (OutboxMessage message in outboxMessages)
                {
                    this._logger.Log(LogLevel.Information, message.ToString());

                    try
                    {
                        var (obj, type) = this.GetOutboxObjectAndType(message.Data, message.TypeName);

                        await this._bus.Publish(obj, type);

                        message.Published();
                    }
                    catch (Exception ex)
                    {
                        message.Failed(ex.Message);
                    }

                    await outboxDbContext.SaveChangesAsync();
                }

                await Task.Delay(5000);
            }
        }

        private (object, Type)  GetOutboxObjectAndType(
            string data,
            string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("Received outbox message type name is null or empty.");
            }

            Type? type = Type.GetType(typeName);

            if (type == null)
            {
                throw new Exception("Received outbox message type name could not be resolved.");
            }

            object? obj = null;

            try
            {
                obj = System.Text.Json.JsonSerializer.Deserialize(data, type);

                if (obj == null)
                {
                    throw new Exception($"Outbox message could not be deserialized to {type.FullName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return (obj, type);
        }
    }
}
