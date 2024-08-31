using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyContacts.Data.DAL;
using MyContacts.Services;

namespace MyContacts;

class TestClass
{
    private List<int> Numbers = new List<int>();

    public List<int> GetList() => Numbers;
}
public class Program
{
    public static void Main(string[] args)
    {
        var a = new TestClass();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.LoginPath = new PathString("/auth/login");
                
            });
        
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDbContext<ContactContext>(
            dbContextOptions => dbContextOptions.UseMySql(
                    $"server=localhost;user=root;database=db;password={builder.Configuration["SQL:UserPwd"]};", 
                                new MySqlServerVersion("8.0.39.0")
                    )
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableSensitiveDataLogging()
                );
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        AddRepositoryServices(builder.Services);
        AddBusinessServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCookiePolicy();
        app.MapControllers();
        
        app.Run();
    }

    private static void AddBusinessServices(IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
    }

    private static void AddRepositoryServices(IServiceCollection services)
    {
        services.AddTransient<IContactRepository, ContactRepository>();
    }
}