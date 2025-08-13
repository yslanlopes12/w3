using Api.Dto;

namespace Domain.Services
{
    public interface IPixKeyService
    {
        Task<PixKeyResponseDto> CreateAsync(PixKeyCreateDto dto);
        Task<PixKeyValidationDto> ValidateAsync(string chave);
        Task<bool> CancelAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, PixKeyUpdateDto dto);
        Task<IEnumerable<PixKeyResponseDto>> GetByAccountIdAsync(Guid accountId);
    }
}


/*
public interface IPixKeyService
{
    Task<PixKeyDto> CreateAsync(PixKeyCreateDto dto);
    Task<bool> ValidateAsync(string chave);
    Task<IEnumerable<PixKeyDto>> GetByAccountIdAsync(Guid accountId);
    Task<bool> CancelAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, PixKeyUpdateDto dto);
}
*/