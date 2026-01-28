using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    // GET /families/{familyId}/members
    // Retorna todos os membros de uma família
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers(Guid familyId)
    {
        var members = await _memberService.GetMembersByFamilyIdAsync(familyId);
        return Ok(members);
    }

    // POST /families/{familyId}/members
    // Cria um novo membro para a família
    [HttpPost]
    public async Task<IActionResult> CreateMember(Guid familyId, Member member)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdMember = await _memberService.CreateMemberAsync(familyId, member);
        if (createdMember == null) return NotFound("Família não encontrada");

        return CreatedAtAction(nameof(GetMembers), new { familyId }, createdMember);
    }
}
