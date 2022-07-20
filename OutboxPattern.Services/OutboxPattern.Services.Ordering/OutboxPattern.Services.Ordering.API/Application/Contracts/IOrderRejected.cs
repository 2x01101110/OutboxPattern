namespace OutboxPattern.Services.Ordering.API.Application.Contracts
{
    public interface IOrderRejected
    {
        string Reason { get; }
    }
}
