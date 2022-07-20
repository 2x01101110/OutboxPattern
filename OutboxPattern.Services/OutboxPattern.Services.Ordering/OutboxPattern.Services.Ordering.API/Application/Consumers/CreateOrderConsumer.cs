using MassTransit;
using OutboxPattern.Services.Ordering.API.Application.Contracts;
using OutboxPattern.Services.Ordering.API.Domain.Orders;
using OutboxPattern.Shared.BuildingBlocks.Outbox;
using OutboxPattern.Shared.Outbox;

namespace OutboxPattern.Services.Ordering.API.Application.Consumers
{
    public class CreateOrderConsumer :
        IConsumer<ICreateOrder>
    {
        private readonly IOrdersRepository _ordersPreository;

        public CreateOrderConsumer(
            IOrdersRepository ordersPreository)
        {
            _ordersPreository = ordersPreository;
        }

        public async Task Consume(ConsumeContext<ICreateOrder> context)
        {
            try
            {
                var order = Order.Create(context.Message.Id, context.Message.Items);

                var outboxMessage = OutboxMessage.Create(
                    context.Message.Id,
                    new OrderCreated(context.Message.Id));

                _ordersPreository.AddEntity(order, new List<OutboxMessage> { outboxMessage });

                await _ordersPreository.SaveChangesAsync();

                await context.RespondAsync<IOrderCreated>(new
                {
                    context.Message.Id
                });
            }
            catch (Exception ex)
            {
                await context.RespondAsync<IOrderRejected>(new
                {
                    Reason = ex.Message
                });
            }
        }
    }
}
