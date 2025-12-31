using FamilyFinance.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Family> Families { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
}
