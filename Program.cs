using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Services;
using Services;
using Domain.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<SqlContext>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var context = sp.GetRequiredService<SqlContext>();
    return context.CreateConnection();
});


builder.Services.AddScoped<IPixKeyRepository, PixKeyRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


builder.Services.AddScoped<IPixKeyService, PixKeyService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
