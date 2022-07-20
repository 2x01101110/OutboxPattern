using Microsoft.EntityFrameworkCore;
using OutboxPattern.Shared.BuildingBlocks.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboxPattern.Services.Ordering.OutboxWorker
{
    internal class OutboxDbContext : DbContext
    {
        public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
        {

        }

        public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        }
    }
}
