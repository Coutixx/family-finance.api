namespace FamilyFinance.Api.Models;

// Enum para o tipo de movimentação
public enum TransactionType
{
    Income,
    Expense
}

// Classe de Categoria (Permite que adicione cores/ícones no futuro)
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class Transaction
{
    // Id (GUID)
    public Guid Id { get; set; }

    // MemberId (FK)
    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; }

    // FamilyId (FK)
    public Guid FamilyId { get; set; }
    public virtual Family Family { get; set; }

    // Type (Enum: Income | Expense)
    public TransactionType Type { get; set; }

    // CategoryId (FK class Category)

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }

    // Amount (decimal)
    public decimal Amount { get; set; }

    // Date (Dia)
    public DateOnly Date { get; set; }

    // Description (string opcional, ? para permitir NULL no banco)
    public string? Description { get; set; }
}
