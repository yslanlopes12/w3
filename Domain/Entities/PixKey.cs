using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PixKey
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Key { get; set; } = string.Empty;
        public PixKeyType Type { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CancelledAt { get; set; }
    }
}
