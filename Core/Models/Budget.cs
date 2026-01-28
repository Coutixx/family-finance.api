namespace FamilyFinance.Api.Core.Models
{
    // Orçamento de uma família para uma categoria em um mês/ano
    public class Budget
    {
        public Guid Id { get; set; }            // Identificador único do orçamento
        public Guid FamilyId { get; set; }      // FK para Family
        public virtual Family Family { get; set; }  // Navegação para a família
        public Guid CategoryId { get; set; }    // FK para Category
        public virtual Category Category { get; set; } // Navegação para a categoria
        public decimal LimitAmount { get; set; } // Valor limite do orçamento
        public int Month { get; set; }           // Mês do orçamento (1-12)
        public int Year { get; set; }            // Ano do orçamento
    }

    // DTO usado para retornar alertas de orçamento
    public class BudgetAlert
    {
        public string Category { get; set; } = null!; // Nome da categoria
        public decimal LimitAmount { get; set; }     // Valor limite definido
        public decimal TotalSpent { get; set; }      // Total gasto na categoria no período
    }
}
