using MyContacts.DTO;

namespace MyContacts.Repositories;

public interface IContactRepository : IDisposable
{
    IEnumerable<Contact> GetContacts();
    Contact GetContactByID(int id);
    Contact GetContactByName(string name);
    Contact GetContactByPhoneNumber(string phoneNumber);
    void InsertContact(Contact contact);
    void UpdateContact(Contact contact);
    void DeleteContact(int contactID);
    void Save();
}