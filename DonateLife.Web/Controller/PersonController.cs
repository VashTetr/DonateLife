using DonateLife.Common.Entity;
using DonateLife.Common.Interface;
using DonateLife.Web.Controllers;
using DonateLife.Web.Options;
using Microsoft.AspNetCore.Mvc;

namespace DonateLife.Web.Controller;

public class PersonController : DonateLifeController
{
    public PersonController(IAccountRepository accountRepository, IPersonRepository personRepository) : base(accountRepository, personRepository)
    {
        [HttpPost]
        [Route("/person/create")]
        public Person CreatePerson([FromForm] CreatePersonOptions personOptions)
        {
            return null;
        }
    }
}