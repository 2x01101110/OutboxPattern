using OutboxPattern.Shared.BuildingBlocks.Domain;

namespace OutboxPattern.Services.Ordering.API.Domain.Orders
{
    public class Order : AggregateRoot
    {
        public List<OrderItem> Items { get; }

        private Order(
            Guid id,
            List<OrderItem> items) : base(id)
        {
            Items = items;
        }

        public static Order Create(
            Guid id,
            List<OrderItem> items)
        {
            return new Order(id, items);
        }

        public Order() : base()
        {

        }
    }
}
