using Dapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PixKeyRepository : IPixKeyRepository
    {
        private readonly DapperContext _context;

        public PixKeyRepository(DapperContext context)
        {
            _context = context;
        }

        // Método Add
        public async Task<Guid> AddAsync(PixKey pixKey)
        {
            var sql = @"
INSERT INTO chave_pix (account_id, pix_type, chave_valor, status, data_criacao)
OUTPUT INSERTED.id
VALUES (@AccountId, @PixType, @ChaveValor, @Status, @DataCriacao);";

            using var connection = _context.CreateConnection();
            pixKey.Id = await connection.ExecuteScalarAsync<Guid>(sql, new
            {
                pixKey.AccountId,
                pixKey.PixType,
                pixKey.ChaveValor,
                pixKey.Status,
                pixKey.DataCriacao
            });

            return pixKey.Id;
        }

        // Implementando GetByIdAsync usando a mesma lógica do GetByKeyAsync
        public async Task<PixKey?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM chave_pix WHERE id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<PixKey>(sql, new { Id = id });
        }

        // GetByKeyAsync já existente
        public async Task<PixKey?> GetByKeyAsync(string chaveValor)
        {
            var sql = "SELECT * FROM chave_pix WHERE chave_valor = @ChaveValor";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<PixKey>(sql, new { ChaveValor = chaveValor });
        }

        // GetByAccountIdAsync já existente
        public async Task<IEnumerable<PixKey>> GetByAccountIdAsync(Guid accountId)
        {
            var sql = "SELECT * FROM chave_pix WHERE account_id = @AccountId";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<PixKey>(sql, new { AccountId = accountId });
        }

        // CancelAsync já existente
        public async Task<int> CancelAsync(Guid id)
        {
            var sql = "UPDATE chave_pix SET status = 0, data_cancelamento = GETDATE() WHERE id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, new { Id = id });
        }

        // Implementando UpdateAsync usando a mesma lógica de Update que comentamos antes
        public async Task UpdateAsync(PixKey pixKey)
        {
            var sql = @"
UPDATE chave_pix
SET 
    pix_type = @PixType,
    chave_valor = @ChaveValor,
    status = @Status,
    data_cancelamento = @DataCancelamento
WHERE id = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                pixKey.PixType,
                pixKey.ChaveValor,
                pixKey.Status,
                pixKey.DataCancelamento,
                pixKey.Id
            });
        }
        Task IPixKeyRepository.AddAsync(PixKey pixKey)
        {
            return AddAsync(pixKey);
        }
    }
}
