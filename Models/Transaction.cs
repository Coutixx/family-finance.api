namespace FamilyFinance.Api.Models;

// Enum para o tipo de movimentação
public enum TransactionType
{
    Income,
    Expense
}

public class Transaction
{
    public Guid Id { get; set; }

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public Guid FamilyId { get; set; }
    public Family Family { get; set; } = null!;

    public TransactionType Type { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }
}
