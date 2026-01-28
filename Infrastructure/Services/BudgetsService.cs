using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Infrastructure.Services
{
    public class BudgetsService : IBudgetsService
    {
        private readonly AppDbContext _context;

        public BudgetsService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os orçamentos de uma família
        public async Task<IEnumerable<Budget>> GetBudgetsByFamilyAsync(Guid familyId)
        {
            return await _context.Budgets
                .Where(b => b.FamilyId == familyId)
                .ToListAsync();
        }

        // Cria ou atualiza um orçamento
        public async Task<Budget> CreateOrUpdateBudgetAsync(Guid familyId, Budget budget)
        {
            var existingBudget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.FamilyId == familyId &&
                                          b.Category == budget.Category &&
                                          b.Month == budget.Month &&
                                          b.Year == budget.Year);

            if (existingBudget != null)
            {
                existingBudget.LimitAmount = budget.LimitAmount;
                _context.Update(existingBudget);
            }
            else
            {
                budget.Id = Guid.NewGuid();
                budget.FamilyId = familyId;
                _context.Add(budget);
            }

            await _context.SaveChangesAsync();
            return budget;
        }

        // Retorna alertas: categorias que excederam o orçamento no mês/ano
        public async Task<IEnumerable<BudgetAlert>> GetBudgetAlertsAsync(Guid familyId, int month, int year)
        {
            var budgets = await _context.Budgets
                .Where(b => b.FamilyId == familyId && b.Month == month && b.Year == year)
                .ToListAsync();

            var expensesByCategory = await _context.Transactions
                .Where(t => t.FamilyId == familyId &&
                            t.Type == TransactionType.Expense &&
                            t.Date.Month == month &&
                            t.Date.Year == year)
                .GroupBy(t => t.Category.Name)  // Usa o nome da categoria para comparar
                .Select(g => new { Category = g.Key, TotalSpent = g.Sum(t => t.Amount) })
                .ToListAsync();

            var alerts = budgets
                .Select(b => new BudgetAlert
                {
                    Category = b.Category.Name,
                    LimitAmount = b.LimitAmount,
                    TotalSpent = expensesByCategory.FirstOrDefault(e => e.Category == b.Category.Name)?.TotalSpent ?? 0
                })
                .Where(a => a.TotalSpent > a.LimitAmount)
                .ToList();

            return alerts;
        }
    }
}
