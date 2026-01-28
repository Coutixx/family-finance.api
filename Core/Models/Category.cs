namespace FamilyFinance.Api.Core.Models
{
    // Categoria de transações associada a uma família
    public class Category
    {
        public Guid Id { get; set; }               // Identificador único da categoria
        public string Name { get; set; } = null!; // Nome da categoria
        public Guid FamilyId { get; set; }         // FK para a família à qual pertence
        public virtual Family Family { get; set; } // Navegação para a família
    }
}
