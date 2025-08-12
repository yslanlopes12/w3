using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PixKey> PixKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento opcional
            modelBuilder.Entity<PixKey>().ToTable("pix_keys");
        }
    }
}
