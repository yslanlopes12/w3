using Dapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PixKeyRepository
    {
        private readonly DapperContext _context;

        public PixKeyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(PixKey pixKey)
        {
            var sql = @"
                INSERT INTO pix_keys (id, account_id, key, type, active, created_at)
                VALUES (@Id, @AccountId, @Key, @Type, @Active, @CreatedAt)";
            
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, pixKey);
        }

        public async Task<PixKey?> GetByKeyAsync(string key)
        {
            var sql = "SELECT * FROM pix_keys WHERE key = @Key";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<PixKey>(sql, new { Key = key });
        }

        public async Task<IEnumerable<PixKey>> GetByAccountIdAsync(Guid accountId)
        {
            var sql = "SELECT * FROM pix_keys WHERE account_id = @AccountId";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<PixKey>(sql, new { AccountId = accountId });
        }

        public async Task<int> CancelAsync(Guid id)
        {
            var sql = "UPDATE pix_keys SET active = false, cancelled_at = NOW() WHERE id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
