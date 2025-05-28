using DonateLife.Common.Entity;

namespace DonateLife.Common.Interface;

public interface IPersonRepository
{
    Person ByEmail(string? email);
    Person Single(Guid id);
}
