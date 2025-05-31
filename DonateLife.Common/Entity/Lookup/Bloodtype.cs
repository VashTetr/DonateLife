using DonateLife.Common.Entity;

public class BloodType : DBEntity
{
	public required Guid ID { get; set; }
    public required string Label { get; set; }
}
