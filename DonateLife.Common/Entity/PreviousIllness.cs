namespace DonateLife.Common.Entity;

public class PreviousIllness : DBEntity
{
    public Patient? Patient { get; set; }
    public Illness? Illness { get; set; }
}
