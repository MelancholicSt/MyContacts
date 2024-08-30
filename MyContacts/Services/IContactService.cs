using MyContacts.DTO;

namespace MyContacts.Services;

public interface IContactService : IDisposable
{
    // User section
    Contact GetContacts();
    void AddContact(Contact contact);
    void RemoveContact(Contact contact);
    void EditContactName(string name);
    void EditContactDescription(string description);
    IEnumerable<Contact> GetContactsWithSameContact();
    
    // Admin section
    void CreateContact(Contact contact);
    void DeleteContact(Contact contact);
}