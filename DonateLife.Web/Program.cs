using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DonateLife.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OpenApi;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Identity;
// using Community.AspNetCore.ExceptionHandling;
using System.Net;
using System.Text;
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
        builder.Services.AddMvc()
        .AddJsonOptions(
            options => {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
                // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                // options.JsonSerializerOptions.PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace;
                // options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip;
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

        builder.Services.AddScoped<DataBase>();
        
        // builder.Services.AddExceptionHandlingPolicies(policies => {
        //     policies.For<NullReferenceException>()
        //         .Response((_) => (int)HttpStatusCode.InternalServerError)
        //         .Headers((settings, exception) => {})
		// 		.WithBody((req, writer, exception) => {
        //             byte[] array = Encoding.UTF8.GetBytes(exception.Message);
        //             return writer.WriteAsync(array, 0, array.Length);
        //         })
        //         .Handled();

        //     policies.For<Exception>()
        //         .Response((_) => (int)HttpStatusCode.InternalServerError)
		// 		.WithBody((req, writer, exception) => {
        //             byte[] array = Encoding.UTF8.GetBytes(exception.Message);
        //             return writer.WriteAsync(array, 0, array.Length);
        //         })
        //         .Handled();
            
        // });

        var app = builder.Build();
        // app.UseExceptionHandlingPolicies();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

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