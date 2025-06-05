using DonateLife.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using DonateLife.DependencyInjection;

namespace DonateLife.Web;

public class Program
{
    public static DataBase context = new DataBase();
    public static void Main(string[] aspNetArgs)
    {
        var builder = WebApplication.CreateBuilder(aspNetArgs);

        ConfigureDependencies.Configure(builder.Services);
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();
        builder.Services.AddMvc()
        .AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
            }
        );

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.LoginPath = "/authentication/login";
            });

        builder.Services.AddSingleton<DataBase>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
internal class Startup // Am I even using this?
{
    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseEndpoints(endpoints => {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default", 
                pattern: "{controller}/{action}");
        });
    }
}
