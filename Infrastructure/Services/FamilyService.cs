using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Infrastructure.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly AppDbContext _context;

        public FamilyService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as famílias
        public async Task<IEnumerable<Family>> GetAllFamiliesAsync()
        {
            return await _context.Families.ToListAsync();
        }

        // Retorna uma família pelo ID
        public async Task<Family> GetFamilyByIdAsync(Guid id)
        {
            return await _context.Families.FindAsync(id);
        }

        // Cria uma nova família
        public async Task<Family> CreateFamilyAsync(Family family)
        {
            family.Id = Guid.NewGuid();
            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            return family;
        }
    }
}
