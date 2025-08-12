using Domain.ValueObjects;

namespace Api.Dto
{
    public class PixKeyCreateDto
    {
        public Guid AccountId { get; set; }
        public string Key { get; set; } = string.Empty;
        public PixKeyType Type { get; set; }
    }
}
