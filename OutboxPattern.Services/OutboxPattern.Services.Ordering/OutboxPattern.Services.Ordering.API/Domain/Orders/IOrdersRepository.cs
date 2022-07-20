using OutboxPattern.Shared.BuildingBlocks.Contracts;

namespace OutboxPattern.Services.Ordering.API.Domain.Orders
{
    public interface IOrdersRepository : IRepository<Order>
    {
    }
}
