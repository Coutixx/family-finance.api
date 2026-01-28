namespace FamilyFinance.Api.Core.Models
{
    // Representa um membro de uma família
    public class Member
    {
        public Guid Id { get; set; }               // Identificador único do membro
        public string Name { get; set; } = null!; // Nome do membro

        // Foreign Key para a família à qual o membro pertence
        public Guid FamilyId { get; set; }
        public virtual Family Family { get; set; } // Navegação para a família
    }
}
