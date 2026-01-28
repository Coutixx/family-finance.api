namespace FamilyFinance.Api.Core.Models
{
    // Representa uma família no sistema
    public class Family
    {
        public Guid Id { get; set; }           // Identificador único da família
        public string Name { get; set; } = null!; // Nome da família
    }
}
