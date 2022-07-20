using Microsoft.EntityFrameworkCore;
using OutboxPattern.Services.Ordering.API.Domain.Orders;
using OutboxPattern.Shared.BuildingBlocks.Outbox;
using System.Reflection;

namespace OutboxPattern.Services.Ordering.API.Infrastructure
{
    public class OrderingDbContext : DbContext
    {
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        }
    }
}
