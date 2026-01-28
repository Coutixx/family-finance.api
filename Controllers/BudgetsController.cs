using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/budgets")]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetsService _budgetService;

    public BudgetsController(IBudgetsService budgetService)
    {
        _budgetService = budgetService;
    }

    // GET /families/{familyId}/budgets
    // Retorna todos os orçamentos da família
    [HttpGet]
    public async Task<IActionResult> GetBudgets(Guid familyId)
    {
        var budgets = await _budgetService.GetBudgetsByFamilyAsync(familyId);
        return Ok(budgets);
    }

    // POST /families/{familyId}/budgets
    // Cria ou atualiza um orçamento
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateBudget(Guid familyId, Budget model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var budget = await _budgetService.CreateOrUpdateBudgetAsync(familyId, model);
        return Ok(budget);
    }

    // GET /families/{familyId}/budgets/alert
    // Retorna alertas de orçamento: categorias que excederam o limite
    [HttpGet("alert")]
    public async Task<IActionResult> GetBudgetAlerts(Guid familyId, [FromQuery] int month, [FromQuery] int year)
    {
        var alerts = await _budgetService.GetBudgetAlertsAsync(familyId, month, year);
        return Ok(alerts);
    }
}
