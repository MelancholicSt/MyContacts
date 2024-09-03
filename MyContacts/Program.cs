using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
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
                o.ExpireTimeSpan = TimeSpan.FromDays(30);
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
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MyContacts API",
                Description = "The simple api for personal phone contacts",
                Contact = new OpenApiContact()
                {
                    Name = "Github",
                    Url = new Uri("https://github.com/MelancholicSt")
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        
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