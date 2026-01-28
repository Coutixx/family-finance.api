using FamilyFinance.Api.Core.Models;

namespace FamilyFinance.Api.Core.Interfaces
{
    // Interface para operações relacionadas a transações financeiras
    public interface ITransactionService
    {
        // Retorna todas as transações de uma família
        Task<IEnumerable<Transaction>> GetTransactionsByFamilyAsync(Guid familyId);

        // Cria uma nova transação
        Task<Transaction> CreateTransactionAsync(Guid familyId, Transaction transaction);

        // Retorna transações filtradas por mês e ano
        Task<IEnumerable<Transaction>> GetMonthlyTransactionsAsync(Guid familyId, int month, int year);

        // Retorna resumo financeiro da família: income, expense e balance
        Task<TransactionSummary> GetSummaryAsync(Guid familyId);
    }
}
