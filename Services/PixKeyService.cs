using Api.Dto;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Domain.Repositories;
using Infrastructure.Repositories;

namespace Services
{
    public class PixKeyService : IPixKeyService
    {
        private readonly IPixKeyRepository _pixKeyRepository;
        private readonly IAccountRepository _accountRepository; // Para validar contas

        public PixKeyService(IPixKeyRepository pixKeyRepository, IAccountRepository accountRepository)
        {
            _pixKeyRepository = pixKeyRepository;
            _accountRepository = accountRepository;
        }

        public async Task<PixKeyResponseDto> CreateAsync(PixKeyCreateDto dto)
{
    var account = await _accountRepository.GetByIdAsync(dto.AccountId);
    if (account is null)
        throw new Exception("Conta não encontrada.");

    // Gere a chave Pix automaticamente (exemplo simples, use sua lógica)
    var chavePix = Guid.NewGuid().ToString();

    var pixKey = new PixKey
    {
        AccountId = dto.AccountId,
        ChaveValor = chavePix,
        PixType = (short)dto.Type,
        Status = true,
        DataCriacao = DateTime.UtcNow
    };

    await _pixKeyRepository.AddAsync(pixKey);

    return new PixKeyResponseDto
    {
        Id = pixKey.Id, // O banco pode retornar o id gerado, se configurado
        ChaveValor = pixKey.ChaveValor,
        PixType = (PixKeyType)pixKey.PixType,
        Status = pixKey.Status
    };
}

        public async Task<bool> CancelAsync(Guid id)
        {
            var pixKey = await _pixKeyRepository.GetByIdAsync(id);
           if (pixKey == null || !pixKey.Status) // Corrigido
    return false;

pixKey.Status = false; // Corrigido
pixKey.DataCancelamento = DateTime.UtcNow; // Corrigido

await _pixKeyRepository.UpdateAsync(pixKey);
return true;
        }

        public async Task<PixKeyValidationDto> ValidateAsync(string key)
        {
            var pixKey = await _pixKeyRepository.GetByKeyAsync(key);
            if (pixKey != null && pixKey.Status)
                return new PixKeyValidationDto { Valid = true, Message = "Chave válida." };

            return new PixKeyValidationDto { Valid = false, Message = "Chave inválida ou inativa." };
        }

        public async Task<IEnumerable<PixKeyResponseDto>> GetByAccountIdAsync(Guid accountId)
        {
            var keys = await _pixKeyRepository.GetByAccountIdAsync(accountId);
            return keys.Select(k => new PixKeyResponseDto
            {
                Id = k.Id,
                AccountId = k.AccountId,
                ChaveValor = k.ChaveValor,
                PixType = (PixKeyType)k.PixType,
                Status = k.Status
            });
        }

        public async Task<bool> UpdateAsync(Guid id, PixKeyUpdateDto dto)
        {
            var pixKey = await _pixKeyRepository.GetByIdAsync(id);
            if (pixKey == null)
                return false;

            pixKey.PixType = (short)dto.PixType;
            pixKey.Status = dto.Status;

            await _pixKeyRepository.UpdateAsync(pixKey);
            return true;
        }
    }
}
