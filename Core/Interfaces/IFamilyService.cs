using FamilyFinance.Api.Core.Models;

namespace FamilyFinance.Api.Core.Interfaces
{
    // Interface para operações relacionadas a famílias
    public interface IFamilyService
    {
        // Retorna todas as famílias
        Task<IEnumerable<Family>> GetAllFamiliesAsync();

        // Retorna uma família pelo ID
        Task<Family> GetFamilyByIdAsync(Guid id);

        // Cria uma nova família
        Task<Family> CreateFamilyAsync(Family family);
    }
}
