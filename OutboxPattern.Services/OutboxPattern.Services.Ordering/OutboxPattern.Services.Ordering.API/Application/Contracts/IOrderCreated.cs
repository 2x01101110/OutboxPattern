namespace OutboxPattern.Services.Ordering.API.Application.Contracts
{
    public interface IOrderCreated
    {
        Guid Id { get; }
    }
}
