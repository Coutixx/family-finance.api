using FamilyFinance.Api.Data;
using FamilyFinance.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families")]
public class FamiliesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FamiliesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Family>>> FamiliesList()
    {
        var families = await _context.Families.ToListAsync();

        return Ok(families);
    }

    [HttpPost]
    public async Task<IActionResult> FamilyCreate(Family family)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        family.Id = Guid.NewGuid();
        _context.Families.Add(family);
        await _context.SaveChangesAsync();

        return Ok(family);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Family>> FamilyDetails(Guid id)
    {
        var family = await _context.Families.FindAsync(id);

        if (family == null) return NotFound("Família não encontrada");

        return Ok(family);
    }

    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<Member>>> MembersList(Guid id)
    {
        var members = await _context.Members.Where(m => m.FamilyId == id).ToListAsync();

        return Ok(members);
    }

    [HttpPost("{id}/members")]
    public async Task<IActionResult> MemberCreate(Guid id, Member member)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        member.Id = Guid.NewGuid();
        member.FamilyId = id;

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return Ok(member);
    }

}