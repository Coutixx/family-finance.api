using FamilyFinance.Api.Data;
using FamilyFinance.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/budgets")]
public class BudgetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BudgetsController(AppDbContext context)
    {
        _context = context;
    }
    // GET /families/{id}/budgets
    // Lista todos os orçamentos da família
    [HttpGet]
    public IActionResult GetBudgets(Guid familyId)
    {
        var budgets = _context.Budgets
            .Where(b => b.FamilyId == familyId)
            .ToList();

        return Ok(budgets);
    }

    // POST /families/{id}/budgets
    // Cria ou atualiza um orçamento
    [HttpPost]
    public IActionResult SaveBudget(Guid familyId, [FromBody] Budget model)
    {
        var existingBudget = _context.Budgets
            .FirstOrDefault(b => b.FamilyId == familyId &&
                                 b.Category == model.Category &&
                                 b.Month == model.Month &&
                                 b.Year == model.Year);

        if (existingBudget != null)
        {
            existingBudget.LimitAmount = model.LimitAmount;
            _context.Update(existingBudget);
        }
        else
        {
            model.FamilyId = familyId;
            model.Id = Guid.NewGuid();
            _context.Add(model);
        }

        _context.SaveChanges();
        return Ok(model);
    }

    // GET /families/{id}/budgets/alert
    // Verifica quais categorias estouraram o limite no mês/ano atual
    [HttpGet("alert")]
    public IActionResult GetBudgetAlerts(Guid familyId, [FromQuery] int month, [FromQuery] int year)
    {
        // 1. Pega os limites definidos
        var budgets = _context.Budgets
            .Where(b => b.FamilyId == familyId && b.Month == month && b.Year == year)
            .ToList();

        // 2. Pega o total de gastos (Expenses) por categoria no período
        var expensesByCategory = _context.Transactions
            .Where(t => t.FamilyId == familyId &&
                        t.Type == TransactionType.Expense &&
                        t.Date.Month == month &&
                        t.Date.Year == year)
            .GroupBy(t => t.Category)
            .Select(g => new { Category = g.Key, TotalSpent = g.Sum(t => t.Amount) })
            .ToList();

        // 3. Filtra apenas os que estouraram
        var alerts = budgets
            .Select(b => new
            {
                b.Category,
                b.LimitAmount,
                TotalSpent = expensesByCategory.FirstOrDefault(e => e.Category == b.Category)?.TotalSpent ?? 0
            })
            .Where(a => a.TotalSpent > a.LimitAmount)
            .ToList();

        return Ok(alerts);
    }
}

