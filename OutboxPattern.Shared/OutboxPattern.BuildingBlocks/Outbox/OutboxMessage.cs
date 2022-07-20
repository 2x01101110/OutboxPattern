using System.Text.Json;

namespace OutboxPattern.Shared.BuildingBlocks.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; }
        public DateTime Timestamp { get; }
        public string TypeName { get; }
        public Status Status { get; private set; }
        public string Data { get; }
        public string? FailedReason { get; private set; }

        public OutboxMessage(
            Guid id,
            string typeName,
            string data)
        {
            Id = id;
            Timestamp = DateTime.UtcNow;
            TypeName = typeName;
            Status = Status.Created;
            Data = data;
        }

        public static OutboxMessage Create<T>(Guid id, T data) where T : class
        {
            Type type = data.GetType();
            string? typeName = type.AssemblyQualifiedName;

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException("AssemblyQualifiedName is null or empty");
            }

            var dataSerialized = JsonSerializer.Serialize(data);

            return new OutboxMessage(id, typeName, dataSerialized);
        }

        public void Published() => Status = Status.Published;
        public void Failed(string? failedReason = null)
        {
            FailedReason = failedReason;
            Status = Status.Failed;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        private OutboxMessage()
        {

        }
    }

    public enum Status
    {
        Unknown,
        Created,
        Published,
        Failed
    }
}
