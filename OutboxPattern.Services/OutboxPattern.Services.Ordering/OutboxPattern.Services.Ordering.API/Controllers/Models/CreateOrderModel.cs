using OutboxPattern.Services.Ordering.API.Application.Contracts;
using OutboxPattern.Services.Ordering.API.Domain.Orders;

namespace OutboxPattern.Services.Ordering.API.Controllers.Models
{
    public class CreateOrderModel : ICreateOrder
    {
        public Guid Id { get; }

        public List<OrderItem> Items { get; }

        public CreateOrderModel(
            Guid id,
            List<OrderItem> items)
        {
            Id = id;
            Items = items;
        }
    }
}
