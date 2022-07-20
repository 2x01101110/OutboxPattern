using OutboxPattern.Shared.BuildingBlocks.Domain;
using OutboxPattern.Shared.BuildingBlocks.Outbox;

namespace OutboxPattern.Shared.BuildingBlocks.Contracts
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T?> GetEntityAsync(
            Guid id);

        void AddEntity(
            T entity);

        void AddEntity(
            T entity,
            IEnumerable<OutboxMessage> outboxMessages);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
