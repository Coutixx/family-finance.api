using FamilyFinance.Api.Data;
using FamilyFinance.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> TransactionsList(Guid familyId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.FamilyId == familyId)
            .Include(t => t.Category)
            .Include(t => t.Member)
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> TransactionCreate(Guid familyId, Transaction transaction)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        transaction.Id = Guid.NewGuid();
        transaction.FamilyId = familyId;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(transaction);
    }

    [HttpGet("{month:int}/{year:int}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> MonthlyStatement(
        Guid familyId,
        int month,
        int year)
    {
        var transactions = await _context.Transactions
            .Where(t =>
                t.FamilyId == familyId &&
                t.Date.Month == month &&
                t.Date.Year == year)
            .Include(t => t.Category)
            .Include(t => t.Member)
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary(Guid familyId)
    {
        var income = await _context.Transactions
            .Where(t => t.FamilyId == familyId && t.Type == TransactionType.Income)
            .SumAsync(t => t.Amount);

        var expense = await _context.Transactions
            .Where(t => t.FamilyId == familyId && t.Type == TransactionType.Expense)
            .SumAsync(t => t.Amount);

        return Ok(new
        {
            income,
            expense,
            balance = income - expense
        });
    }
}
