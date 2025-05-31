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

    public Account? GetLogin(string email, string passwordHash)
    {
        return ( 
            from person in _context.Set<Person>().AsQueryable()
            join account in _context.Set<Account>().AsQueryable() on person.AccountID equals account.ID
            where person.EMail == email && person.Account!.PasswordHash == passwordHash
            select person.Account
        ).FirstOrDefault();
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
