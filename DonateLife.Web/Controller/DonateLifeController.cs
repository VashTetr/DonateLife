using System.Reflection;
using DonateLife.Common.Entity;
using DonateLife.Common.Interface;
using DonateLife.Infrastructure;
using DonateLife.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonateLife.Web.Controllers;

[ApiController]
public class DonateLifeController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPersonRepository _personRepository;

    public DonateLifeController(IAccountRepository accountRepository, IPersonRepository personRepository)
    {
        _accountRepository = accountRepository;
        _personRepository = personRepository;
    }

    /// <summary>
    /// Gets the currently logged in User.
    /// Has Person filled in.
    /// </summary>
    /// <returns></returns>
    internal Person? getCurrentUser() {
        var email = HttpContext.User.Identity.Name;
        Person user = _personRepository.ByEmail(email);
        if (user is null) { return null; }
        user.Account.GetType();
        return user;
    }
}
