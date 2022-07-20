namespace OutboxPattern.Shared.Outbox
{
    public class OrderCreated
    {
        public Guid OrderId { get; }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
