using FamilyFinance.Api.Core.Models;

namespace FamilyFinance.Api.Core.Interfaces
{
    // Interface para operações relacionadas a orçamentos
    public interface IBudgetsService
    {
        // Retorna todos os orçamentos de uma família
        Task<IEnumerable<Budget>> GetBudgetsByFamilyAsync(Guid familyId);

        // Cria ou atualiza um orçamento
        Task<Budget> CreateOrUpdateBudgetAsync(Guid familyId, Budget budget);

        // Retorna alertas de orçamento: categorias que excederam o limite
        Task<IEnumerable<BudgetAlert>> GetBudgetAlertsAsync(Guid familyId, int month, int year);
    }
}
