using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyContacts.Data.Models;

namespace MyContacts.Data.DAL;
#pragma warning disable CS1591
public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<ContactFriend> ContactFriends { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ContactFriend>(b =>
        {
            b.HasKey(e => new { e.ContactId, e.FriendId });
            b.HasOne(e => e.Contact).WithMany(e => e.Friends);
            b.HasOne(e => e.Friend).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
    
}
#pragma warning restore CS1591