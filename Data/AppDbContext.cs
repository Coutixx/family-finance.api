using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Data
{
    // Contexto do Entity Framework Core para o FamilyFinance
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Tabelas do banco de dados
        public DbSet<Family> Families { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Budget> Budgets { get; set; } = null!;

        // Salva as alterações no banco de forma assíncrona
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
