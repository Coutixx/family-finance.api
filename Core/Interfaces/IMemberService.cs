using FamilyFinance.Api.Core.Models;

namespace FamilyFinance.Api.Core.Interfaces
{
    // Interface para operações relacionadas a membros de famílias
    public interface IMemberService
    {
        // Retorna todos os membros de uma família
        Task<IEnumerable<Member>> GetMembersByFamilyIdAsync(Guid familyId);

        // Cria um novo membro para uma família
        Task<Member> CreateMemberAsync(Guid familyId, Member member);
    }
}
