using OutboxPattern.Services.Ordering.API.Domain.Orders;

namespace OutboxPattern.Services.Ordering.API.Application.Contracts
{
    public interface ICreateOrder
    {
        Guid Id { get; }
        List<OrderItem> Items { get; }
    }
}
