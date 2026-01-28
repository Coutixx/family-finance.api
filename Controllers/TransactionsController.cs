using FamilyFinance.Api.Core.Models;
using FamilyFinance.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // GET /families/{familyId}/transactions
    // Retorna todas as transações de uma família
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(Guid familyId)
    {
        var transactions = await _transactionService.GetTransactionsByFamilyAsync(familyId);
        return Ok(transactions);
    }

    // GET /families/{familyId}/transactions/{month}/{year}
    // Retorna todas as transações de um mês/ano específico
    [HttpGet("{month:int}/{year:int}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetMonthlyStatement(
        Guid familyId, int month, int year)
    {
        var transactions = await _transactionService.GetMonthlyTransactionsAsync(familyId, month, year);
        return Ok(transactions);
    }

    // GET /families/{familyId}/transactions/summary
    // Retorna o resumo financeiro da família: income, expense e balance
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(Guid familyId)
    {
        var summary = await _transactionService.GetSummaryAsync(familyId);
        return Ok(summary);
    }

    // POST /families/{familyId}/transactions
    // Cria uma nova transação para a família
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(Guid familyId, Transaction transaction)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdTransaction = await _transactionService.CreateTransactionAsync(familyId, transaction);
        return CreatedAtAction(nameof(GetTransactions), new { familyId }, createdTransaction);
    }
}
