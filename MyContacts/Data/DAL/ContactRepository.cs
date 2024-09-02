using Microsoft.EntityFrameworkCore;
using MyContacts.Data.Models;

namespace MyContacts.Data.DAL;

public class ContactRepository(ContactContext context) : IContactRepository
{
    private ContactContext context = context;
    public IEnumerable<Contact> GetContacts()
    {
        return context.Contacts.ToList();
    }

    public Contact? GetContactByID(int id)
    {
        return context.Contacts.Find(id);
    }

    public Contact? GetContactByName(string name)
    {
        return context.Contacts.FirstOrDefault(contact => contact.Name == name);
    }

    public Contact? GetContactByPhoneNumber(string phoneNumber)
    {
        return context.Contacts.FirstOrDefault(contact => contact.PhoneNumber == phoneNumber);
    }

    public void InsertContact(Contact contact)
    {
        context.Contacts.Add(contact);
        Save();
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
        
        GC.SuppressFinalize(this);
    }

}