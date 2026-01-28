namespace FamilyFinance.Api.Core.Models
{
    // Enum para o tipo de movimentação financeira
    public enum TransactionType
    {
        Income,  // Receita
        Expense  // Despesa
    }

    // Representa uma transação financeira de um membro de uma família
    public class Transaction
    {
        public Guid Id { get; set; }                  // Identificador único da transação

        // Membro responsável pela transação
        public Guid MemberId { get; set; }
        public virtual Member Member { get; set; } = null!;

        // Família à qual a transação pertence
        public Guid FamilyId { get; set; }
        public virtual Family Family { get; set; } = null!;

        // Tipo da transação (Income ou Expense)
        public TransactionType Type { get; set; }

        // Categoria associada à transação
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public decimal Amount { get; set; }          // Valor da transação
        public DateOnly Date { get; set; }           // Data da transação
        public string Description { get; set; } = null!; // Descrição ou observação
    }
}
