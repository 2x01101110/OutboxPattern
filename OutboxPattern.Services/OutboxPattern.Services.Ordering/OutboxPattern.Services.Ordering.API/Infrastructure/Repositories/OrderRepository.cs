using Microsoft.EntityFrameworkCore;
using OutboxPattern.Services.Ordering.API.Domain.Orders;
using OutboxPattern.Services.Ordering.API.Infrastructure;
using OutboxPattern.Shared.BuildingBlocks.Outbox;

namespace OutboxPattern.Services.Ordering.API.Infrastructure.Repositories
{
    public class OrderRepository : IOrdersRepository
    {
        private readonly OrderingDbContext _dbContext;

        public OrderRepository(
            OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEntity(
            Order entity)
        {
            _dbContext.Orders.Add(entity);
        }

        public void AddEntity(
            Order entity,
            IEnumerable<OutboxMessage> outboxMessages)
        {
            //MassTransit.IPublishEndpoint endpoint;

            //endpoint.Publish()

            AddEntity(entity);

            foreach (var outboxMessage in outboxMessages)
            {
                _dbContext.Outbox.Add(outboxMessage);
            }
        }

        public Task<Order?> GetEntityAsync(
            Guid id)
        {
            return _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
