using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using DonateLife.Common.Entity;
using DonateLife.Infrastructure.Data;
using DonateLife.Common.Interface;

namespace DonateLife.Web.Controllers;

[ApiController]
public class AuthenticationController : DonateLifeController
{
    private readonly IAccountRepository _accountRepository;

    public AuthenticationController(IAccountRepository accountRepository, IPersonRepository personRepository) : base(accountRepository, personRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost]
    [Route("authentication/login")]
    public Account Login(string email, string password)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
        byte[] passwordHash = System.Security.Cryptography.SHA256.HashData(bytes);
        
        Account user = _accountRepository.GetLogin(email, BitConverter.ToString(passwordHash));
        
        Claim[] claims = [new Claim(ClaimTypes.Name, email)];
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        HttpContext.SignInAsync(principal).Wait();
        HttpContext.AuthenticateAsync().Wait();

        return user;
    }
}
