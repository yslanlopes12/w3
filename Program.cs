using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Services;
using Services;
using Domain.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar DapperContext e IDbConnection para SQL Server
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var context = sp.GetRequiredService<DapperContext>();
    return context.CreateConnection(); // Cria conexão SQL Server
});

// Registrar repositórios
builder.Services.AddScoped<IPixKeyRepository, PixKeyRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Registrar serviços
builder.Services.AddScoped<IPixKeyService, PixKeyService>();

// Configurar controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
