using DonateLife.Common.Entity;
using DonateLife.Common.Interface;

namespace DonateLife.Infrastructure.Data;

public class PersonRepository : IPersonRepository
{
    private readonly IDataBase _context;

    public PersonRepository(IDataBase context)
    {
        _context = context;
    }

    public Person ByEmail(string? email) => _context.Set<Person>().Where(p => p.EMail == email).Single();

    public Person Single(Guid id) => _context.Single<Person>(id);
}