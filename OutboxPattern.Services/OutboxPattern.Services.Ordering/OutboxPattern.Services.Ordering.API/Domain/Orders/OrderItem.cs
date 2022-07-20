using System.Text.Json.Serialization;

namespace OutboxPattern.Services.Ordering.API.Domain.Orders
{
    public class OrderItem
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public OrderItem(
            Guid productId,
            int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public OrderItem()
        {

        }
    }
}
