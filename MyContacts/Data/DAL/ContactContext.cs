using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyContacts.Data.Models;

namespace MyContacts.Data.DAL;
#pragma warning disable CS1591
public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Contact>()
            .HasOne(x => x.ParentContact)
            .WithMany(x => x.SubContacts)
            .HasForeignKey(x => x.ParentContactId)
            .IsRequired(false);
    }
}
#pragma warning restore CS1591