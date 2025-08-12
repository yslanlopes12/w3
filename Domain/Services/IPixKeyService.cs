using Api.Dto;

namespace Domain.Services
{
    public interface IPixKeyService
    {
        Task<PixKeyResponseDto> CreateAsync(PixKeyCreateDto dto);
        Task<bool> CancelAsync(Guid id);
        Task<PixKeyValidationDto> ValidateAsync(string key);
        Task<IEnumerable<PixKeyResponseDto>> GetByAccountIdAsync(Guid accountId);
    }
}
