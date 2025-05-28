using DonateLife.Common.Entity;
using DonateLife.Common.Interface;

namespace DonateLife.Infrastructure.Data;

public class AccountRepository : IAccountRepository
{
    private readonly IDataBase _context;

    public AccountRepository(IDataBase context)
    {
        _context = context;
    }

    public void Create(Account account)
    {
        _context.Add(account);
    }

    public Account GetLogin(string email, string passwordHash)
    {
        return new Account(){ PasswordHash = "penis", Id = Guid.NewGuid(), Role = new() { Label = "balls", Id = Guid.NewGuid() } };
        return ( 
            from person in _context.Set<Person>()
            where person.EMail == email && person.Account != null && person.Account.PasswordHash == passwordHash
            select person.Account
        ).Single();
    }

    public Account GetAccount(string email)
    {
        return (
            from person in _context.Set<Person>()
            where person.EMail == email
            select person.Account
        ).Single();
    }
}