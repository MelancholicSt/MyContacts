using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyContacts.Data.Models;

namespace MyContacts.Data.DAL;

public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Contact>(e =>
        {
            e.HasMany<Contact>().WithOne();
            e.HasKey(c => c.Id);
        });
    }
}