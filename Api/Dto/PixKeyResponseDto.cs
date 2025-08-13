using Domain.ValueObjects;

namespace Api.Dto
{
    public class PixKeyResponseDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string ChaveValor { get; set; } = string.Empty;
        public PixKeyType PixType { get; set; }
        public bool Status { get; set; }
    }
}
