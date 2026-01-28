using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Infrastructure.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os membros de uma família
        public async Task<IEnumerable<Member>> GetMembersByFamilyIdAsync(Guid familyId)
        {
            return await _context.Members
                .Where(m => m.FamilyId == familyId)
                .ToListAsync();
        }

        // Cria um novo membro para uma família
        public async Task<Member> CreateMemberAsync(Guid familyId, Member member)
        {
            var family = await _context.Families.FindAsync(familyId);
            if (family == null) return null;

            member.Id = Guid.NewGuid();
            member.FamilyId = familyId;

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }
    }
}
