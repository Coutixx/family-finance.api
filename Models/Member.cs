namespace FamilyFinance.Api.Models;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    // FamilyId (FK)
    public Guid FamilyId { get; set; }
    public Family Family { get; set; }


}