using DonateLife.Common.Entity;

namespace DonateLife.Infrastructure.Authorization;

public class PersonAuthorizer : Authorizer<Person>
{
    public bool AllowRead(Person person, AuthContext auth)
    {
        if (person.Id != auth.User.Id) return true;

        return false;
    }
}