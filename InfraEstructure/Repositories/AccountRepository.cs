using System.Data;
using System.Security.Principal;
using Dapper;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
   public class AccountRepository : IAccountRepository
{
    private readonly IDbConnection _connection;

    public AccountRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<PixKey?> GetByIdAsync(Guid id) // Corrigido o tipo de retorno
    {
        string sql = "SELECT * FROM contas WHERE id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<PixKey?>(sql, new { Id = id });
    }
}
}
