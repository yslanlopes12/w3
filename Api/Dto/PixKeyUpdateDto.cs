using Domain.ValueObjects;

namespace Api.Dto
{
    public class PixKeyUpdateDto
    {
        public PixKeyType PixType { get; set; }
        public bool Status { get; set; }
    }
}
