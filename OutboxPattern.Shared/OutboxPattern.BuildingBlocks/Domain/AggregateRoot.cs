namespace OutboxPattern.Shared.BuildingBlocks.Domain
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

        protected AggregateRoot()
        {

        }
    }
}
