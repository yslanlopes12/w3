using Api.Dto;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Domain.Repositories;
using InfraStructure.Repositories;

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

            var existingKey = await _pixKeyRepository.GetByKeyAsync(dto.Key);
            if (existingKey != null)
                throw new Exception("Chave PIX já está em uso.");

            var pixKey = new PixKey
            {
                Id = Guid.NewGuid(),
                AccountId = dto.AccountId,
                Key = dto.Key,
                Type = dto.Type
            };

            await _pixKeyRepository.AddAsync(pixKey);

            return new PixKeyResponseDto
            {
                Id = pixKey.Id,
                AccountId = pixKey.AccountId,
                Key = pixKey.Key,
                Type = pixKey.Type,
                Active = pixKey.Active
            };
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var pixKey = await _pixKeyRepository.GetByIdAsync(id);
            if (pixKey == null || !pixKey.Active)
                return false;

            pixKey.Active = false;
            pixKey.CancelledAt = DateTime.UtcNow;

            await _pixKeyRepository.UpdateAsync(pixKey);
            return true;
        }

        public async Task<PixKeyValidationDto> ValidateAsync(string key)
        {
            var pixKey = await _pixKeyRepository.GetByKeyAsync(key);
            if (pixKey != null && pixKey.Active)
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
                Key = k.Key,
                Type = k.Type,
                Active = k.Active
            });
        }
    }
}
