using DonateLife.Common.Entity;

namespace DonateLife.Infrastructure.Authorization;

public struct AuthContext
{
    public Account User;
}

public interface Authorizer<T>
{
    public bool AllowRead(T t, AuthContext auth);
}