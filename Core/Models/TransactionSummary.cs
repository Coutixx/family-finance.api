namespace FamilyFinance.Api.Core.Models
{
    // Resumo financeiro de uma família: total de receitas, despesas e saldo
    public class TransactionSummary
    {
        public decimal Income { get; set; }   // Total de receitas
        public decimal Expense { get; set; }  // Total de despesas

        // Saldo calculado automaticamente (Income - Expense)
        public decimal Balance => Income - Expense;
    }
}
