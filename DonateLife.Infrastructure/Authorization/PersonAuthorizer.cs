using DonateLife.Common.Entity;

namespace DonateLife.Infrastructure.Authorization;

public class PersonAuthorizer : Authorizer<Person>
{
    public bool AllowRead(Person person, AuthContext auth)
    {
        if (person.ID != auth.User.ID) return true;

        return false;
    }
}
