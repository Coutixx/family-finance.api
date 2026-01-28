using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using FamilyFinance.Api.Core.Interfaces;

namespace FamilyFinance.Api.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as categorias de uma família
        public async Task<IEnumerable<Category>> GetCategoriesByFamilyIdAsync(Guid familyId)
        {
            return await _context.Categories
                .Where(c => c.FamilyId == familyId)
                .ToListAsync();
        }

        // Retorna uma categoria específica pelo ID
        public async Task<Category> GetCategoryByIdAsync(Guid familyId, Guid id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.FamilyId == familyId && c.Id == id);
        }

        // Cria uma nova categoria para uma família
        public async Task<Category> CreateCategoryAsync(Guid familyId, Category category)
        {
            category.Id = Guid.NewGuid();
            category.FamilyId = familyId;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
