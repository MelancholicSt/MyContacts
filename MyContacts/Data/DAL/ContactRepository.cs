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
        return context.Contacts.Include(c => c.Friends).FirstOrDefault(x => x.Id == id);
    }

    public Contact? GetContactByName(string name)
    {
        return context.Contacts.Include(x => x.Friends).FirstOrDefault(contact => contact.Name == name);
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
        ContactFriend contactFriend = new ContactFriend()
        {
            ContactId = contact.Id,
            FriendId = subContact.Id
        };
        context.Add(contactFriend);
        context.SaveChanges();
    }

    public void RemoveSubContact(Contact contact, Contact subContact)
    {
        ContactFriend contactFriend = new ContactFriend()
        {
            ContactId = contact.Id,
            FriendId = subContact.Id
        };
        context.ChangeTracker.Clear();
        context.Remove(contactFriend);
        context.SaveChanges();
    }

    public void UpdateSubContact(Contact contact, Contact subContact)
    {
        ContactFriend contactFriend = context.ContactFriends.Find(contact.Id, subContact.Id);
        ContactFriend newValueContactFriend = new ContactFriend()
        {
            Contact = contact,
            Friend = subContact
        };
        context.Entry(contactFriend).CurrentValues.SetValues(newValueContactFriend);
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