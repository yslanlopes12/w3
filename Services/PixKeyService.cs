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
        private readonly IAccountRepository _accountRepository;

        public PixKeyService(IPixKeyRepository pixKeyRepository, IAccountRepository accountRepository)
        {
            _pixKeyRepository = pixKeyRepository;
            _accountRepository = accountRepository;
        }

        // ----------------------------
        // Criar uma nova chave Pix
        // ----------------------------
        public async Task<PixKeyResponseDto> CreateAsync(PixKeyCreateDto dto)
        {
            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account is null)
                throw new Exception("Conta não encontrada.");

            // Gera chave Pix (lógica simples — pode ser substituída por outra)
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
                Id = pixKey.Id,
                ChaveValor = pixKey.ChaveValor,
                PixType = (PixKeyType)pixKey.PixType,
                Status = pixKey.Status
            };
        }

        // ----------------------------
        // Cancelar chave Pix
        // ----------------------------
        public async Task<bool> CancelAsync(Guid id)
        {
            var pixKey = await _pixKeyRepository.GetByIdAsync(id);
            if (pixKey == null || !pixKey.Status)
                return false;

            pixKey.Status = false;
            pixKey.DataCancelamento = DateTime.UtcNow;

            await _pixKeyRepository.UpdateAsync(pixKey);
            return true;
        }

        // ----------------------------
        // Validar chave Pix
        // ----------------------------
        public async Task<PixKeyValidationDto> ValidateAsync(string key)
        {
            var pixKey = await _pixKeyRepository.GetByKeyAsync(key);

            if (pixKey != null && pixKey.Status)
                return new PixKeyValidationDto { Valid = true, Message = "Chave válida." };

            return new PixKeyValidationDto { Valid = false, Message = "Chave inválida ou inativa." };
        }

        // ----------------------------
        // Buscar chaves Pix por conta
        // ----------------------------
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

        // ----------------------------
        // Atualizar chave Pix
        // ----------------------------
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
