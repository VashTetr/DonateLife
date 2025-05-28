using DonateLife.Common.Entity;

namespace DonateLife.Common.Interface;

public interface IAccountRepository
{
    void Create(Account account);
    Account GetAccount(string email);
    Account GetLogin(string email, string passwordHash);
}