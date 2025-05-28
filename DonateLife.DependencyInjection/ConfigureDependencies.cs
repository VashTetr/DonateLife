using DonateLife.Common.Interface;
using DonateLife.Infrastructure;
using DonateLife.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DonateLife.DependencyInjection;

public class ConfigureDependencies
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IDataBase, DataBase>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
