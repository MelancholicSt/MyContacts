using Microsoft.EntityFrameworkCore;
using MyContacts.DTO;

namespace MyContacts.Repositories;

public class ContactRepository(ContactContext context) : IContactRepository
{
    private ContactContext context = context;
    public IEnumerable<Contact> GetContacts()
    {
        return context.Contacts.ToList();
    }

    public Contact GetContactByID(int id)
    {
        return context.Contacts.Find(id);
    }

    public Contact GetContactByName(string name)
    {
        return context.Contacts.First(contact => contact.GetName() == name);
    }

    public Contact GetContactByPhoneNumber(string phoneNumber)
    {
        return context.Contacts.First(contact => contact.GetPhoneNumber() == phoneNumber);
    }

    public void InsertContact(Contact contact)
    {
        context.Contacts.Add(contact);
    }

    public void UpdateContact(Contact contact)
    {
        context.Entry(contact).State = EntityState.Modified;
    }

    public void DeleteContact(int contactID)
    {
        Contact contact = GetContactByID(contactID);
        context.Contacts.Remove(contact);
    }

    public void Save()
    {
        context.SaveChanges();
    }
    public void Dispose()
    {
        Dispose();
        GC.SuppressFinalize(this);
    }

}