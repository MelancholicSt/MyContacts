using MyContacts.Repositories;
using MyContacts.Services;

namespace MyContacts;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ContactContext>();
        // Add services to the container.
        AddRepositoryServices(builder.Services);
        AddBusinessServices(builder.Services);
        

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
    
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        
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