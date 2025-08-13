using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PixKey
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public short PixType { get; set; }
        public string ChaveValor { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataCancelamento { get; set; }
    }
}
