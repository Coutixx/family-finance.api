using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Api.Controllers;

[ApiController]
[Route("families/{familyId}/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET /families/{familyId}/categories
    // Retorna todas as categorias de uma família
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories(Guid familyId)
    {
        var categories = await _categoryService.GetCategoriesByFamilyIdAsync(familyId);
        return Ok(categories);
    }

    // GET /families/{familyId}/categories/{id}
    // Retorna uma categoria específica pelo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(Guid familyId, Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(familyId, id);
        if (category == null) return NotFound("Categoria não encontrada");

        return Ok(category);
    }

    // POST /families/{familyId}/categories
    // Cria uma nova categoria para a família
    [HttpPost]
    public async Task<IActionResult> CreateCategory(Guid familyId, Category category)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdCategory = await _categoryService.CreateCategoryAsync(familyId, category);
        return CreatedAtAction(nameof(GetCategoryById), new { familyId, id = createdCategory.Id }, createdCategory);
    }
}
