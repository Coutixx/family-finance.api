using FamilyFinance.Api.Data;
using FamilyFinance.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/categories")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> CategoriesList(Guid familyId)
    {
        var categories = await _context.Categories
            .Where(c => c.FamilyId == familyId)
            .ToListAsync();

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CategoryCreate(Guid familyId, Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        category.Id = Guid.NewGuid();
        category.FamilyId = familyId;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }
}
