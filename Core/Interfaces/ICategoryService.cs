using FamilyFinance.Api.Core.Models;

namespace FamilyFinance.Api.Core.Interfaces
{
    // Interface para operações relacionadas a categorias de transações
    public interface ICategoryService
    {
        // Retorna todas as categorias de uma família
        Task<IEnumerable<Category>> GetCategoriesByFamilyIdAsync(Guid familyId);

        // Retorna uma categoria pelo ID
        Task<Category> GetCategoryByIdAsync(Guid familyId, Guid id);

        // Cria uma nova categoria
        Task<Category> CreateCategoryAsync(Guid familyId, Category category);
    }
}
