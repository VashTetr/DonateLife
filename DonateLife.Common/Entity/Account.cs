namespace DonateLife.Common.Entity;

public class Account : DBEntity
{
    public required string PasswordHash { get; set; }
    public Role? Role { get; set; }
}
