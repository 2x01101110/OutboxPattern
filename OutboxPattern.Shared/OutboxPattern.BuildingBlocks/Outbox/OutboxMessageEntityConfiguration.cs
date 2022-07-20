using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutboxPattern.Shared.BuildingBlocks.Outbox
{
    public class OutboxMessageEntityConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder
                .ToTable("outboxMessages");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Status)
                .HasColumnName("published");

            builder
                .Property(x => x.Timestamp)
                .HasColumnName("timestamp");

            builder
                .Property(x => x.TypeName)
                .HasColumnName("type");

            builder
                .Property(x => x.Data)
                .HasColumnName("data");

            builder
                .Property(x => x.FailedReason)
                .HasColumnName("failedReason");
        }
    }
}
