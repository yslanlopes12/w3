using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<PixKey?> GetByIdAsync(Guid id);
    }
}
