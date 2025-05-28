namespace DonateLife.Common.Entity;

public class Person : DBEntity
{
    public Patient? Patient { get; set; }
    public Account? Account { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EMail { get; set; }
    public bool IsDeleted { get; set; } = false;
}
