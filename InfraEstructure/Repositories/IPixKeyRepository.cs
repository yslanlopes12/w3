using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IPixKeyRepository
    {
        Task<PixKey?> GetByIdAsync(Guid id);
        Task<PixKey?> GetByKeyAsync(string key);
        Task<IEnumerable<PixKey>> GetByAccountIdAsync(Guid accountId);
        Task AddAsync(PixKey pixKey);
        Task UpdateAsync(PixKey pixKey);
    }
}
