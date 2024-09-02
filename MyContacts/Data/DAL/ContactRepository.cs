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
        return context.Contacts.Include(c => c.SubContacts).FirstOrDefault(x => x.Id == id);
    }

    public Contact? GetContactByName(string name)
    {
        return context.Contacts.Include(c => c.SubContacts).FirstOrDefault(contact => contact.Name == name);
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

    public void InsertSubContact(Contact contact, Contact subContact)
    {
        Contact dbContact = context.Contacts
            .Include(c => c.SubContacts)
            .FirstOrDefault(c => c == contact);
        dbContact.SubContacts.Add(subContact);
        context.SaveChanges();
    }

    public void RemoveSubContact(Contact contact, Contact subContact)
    {
        Contact dbContact = context.Contacts
            .Include(c => c.SubContacts)
            .FirstOrDefault(c => c == contact);
        dbContact.SubContacts.Remove(subContact);
        context.SaveChanges();
    }

    public void UpdateSubContact(Contact contact, Contact subContact)
    {
        Contact existingContact = context.Contacts
            .Include(c => c.SubContacts)
            .FirstOrDefault(c => c == contact);

        Contact editingContact = existingContact.SubContacts.First(c => c.Id == subContact.Id);
        
        context.Entry(editingContact).CurrentValues.SetValues(subContact);
        context.SaveChanges();
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