namespace FamilyFinance.Api.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid FamilyId { get; set; }
}
