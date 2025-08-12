using Domain.ValueObjects;
namespace Domain.Entities
{
    public class PixKey
    {
            public Guid Id { get; private set; }
            public Guid AccountId { get; private set; }

        public string Key { get; set; } = string.Empty;

        public PixKeyType Type { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? CancelledAt { get; private set; }

    }
}