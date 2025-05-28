using DonateLife.Common.Entity;

public class Patient : DBEntity
{
    public required BloodType BloodType { get; set; }
    public required Sex Sex { get; set; }
}