using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families")]
public class FamiliesController : ControllerBase
{
    private readonly IFamilyService _familyService;

    public FamiliesController(IFamilyService familyService)
    {
        _familyService = familyService;
    }

    // GET /families
    // Retorna todas as famílias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Family>>> GetFamilies()
    {
        var families = await _familyService.GetAllFamiliesAsync();
        return Ok(families);
    }

    // GET /families/{id}
    // Retorna uma família específica pelo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Family>> GetFamilyById(Guid id)
    {
        var family = await _familyService.GetFamilyByIdAsync(id);
        if (family == null) return NotFound("Família não encontrada");

        return Ok(family);
    }

    // POST /families
    // Cria uma nova família
    [HttpPost]
    public async Task<IActionResult> CreateFamily(Family family)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdFamily = await _familyService.CreateFamilyAsync(family);
        return CreatedAtAction(nameof(GetFamilyById), new { id = createdFamily.Id }, createdFamily);
    }
}
