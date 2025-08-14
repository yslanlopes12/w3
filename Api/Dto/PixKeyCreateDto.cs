using Domain.ValueObjects;

namespace Api.Dto
{
    public class PixKeyCreateDto
    {
        public Guid AccountId { get; set; }
        public PixKeyType Type { get; set; }
        public string? ChaveValor { get; set; }
    }
}
