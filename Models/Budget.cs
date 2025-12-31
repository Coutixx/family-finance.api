namespace FamilyFinance.Api.Models;

public class Budget
{
    public Guid Id { get; set; }

    //FamilyId (FK)
    public Guid FamilyId { get; set; }
    public virtual Family Family { get; set; }

    // CategoryId (FK)
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public decimal LimitAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}