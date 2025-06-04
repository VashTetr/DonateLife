using DonateLife.Common.Entity;
using DonateLife.Common.Interface;
using DonateLife.Web.Options;
using Microsoft.AspNetCore.Mvc;

namespace DonateLife.Web.Controllers;

public class AccountController : DonateLifeController
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository, IPersonRepository personRepository) : base(accountRepository, personRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost]
    [Route("/account/create")]
    public Account CreateAccount([FromForm]CreateAccountOptions account)
    {
        return null;
    }

    [HttpPost]
    [Route("/account/register")]
    public Account CreateAccountAndPerson([FromForm]CreateAccountAndPersonOptions options)
    {
        return null;
    }

    [HttpPost]
    [Route("/account/delete")]
    public void DeleteAccount(Guid account)
    {

    }
}
