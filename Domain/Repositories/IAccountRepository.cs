using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAccountRepository
{
    Task<PixKey?> GetByIdAsync(Guid id);
}
}
