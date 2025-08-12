using Domain.ValueObjects;

namespace Api.Dto
{
    public class PixKeyResponseDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Key { get; set; } = string.Empty;
        public PixKeyType Type { get; set; }
        public bool Active { get; set; }
    }
}
