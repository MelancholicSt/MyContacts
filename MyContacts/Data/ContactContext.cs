using Microsoft.EntityFrameworkCore;
using MyContacts.DTO;

namespace MyContacts.Repositories;

public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts;
}