using Domain.Entities;
using InfraStructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraEstructure.Repositories
{
    public class PixKeyRepository : IPixKeyRepository
    {
        private readonly AppContext _context;
        public PixKeyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PixKey?> GetByIdAsync(Guid id) =>
        await _context.PixKeys.FirstOrDefaultAsync(p => p.Id == id);
        public async Task<PixKey?> GetByKeyAsync(string key) =>
        await _context.PixKeys.FirstOrDefaultAsync(p => p.Key == key);

        public async Task<IEnumerable<PixKey>> GetByAccountIdAsync(Guid accountId) =>
        await _context.PixKeys.Where(p => p.AccountId == accountId).ToListAsync();

        public async Task AddAsync(PixKey pixKey)
        {
            await _context.PixKeys.AddAsync(pixKey);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PixKey pixKey)
        {
            _context.PixKeys.Update(pixKey);
            await _context.SaveChangesAsync();
        }
    }

}
