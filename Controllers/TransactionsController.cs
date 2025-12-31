using FamilyFinance.Api.Models;
using FamilyFinance.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{id}/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> TransactionsList(Guid id)
    {
        var transactions = await _context.Transactions.Where(t => t.FamilyId == id).OrderByDescending(t => t.Date).ToListAsync();

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> TransactionsRegister(Guid id, [FromBody] Transaction transaction)
    {
        transaction.Id = Guid.NewGuid();
        transaction.FamilyId = id;

        transaction.Member = null;
        transaction.Family = null;
        transaction.Category = null;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(transaction);
    }

    [HttpGet("{month:int}/{year:int}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetByMonth(Guid id, int month, int year)
    {
        return await _context.Transactions
            .Where(t => t.FamilyId == id && t.Date.Month == month && t.Date.Year == year)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(Guid id)
    {
        var transactions = await _context.Transactions
            .Where(t => t.FamilyId == id)
            .ToListAsync();

        // Lógica de cálculo usando o Enum TransactionType
        var totalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var totalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
        var balance = totalIncome - totalExpense;

        return Ok(new
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = balance
        });

    }
}