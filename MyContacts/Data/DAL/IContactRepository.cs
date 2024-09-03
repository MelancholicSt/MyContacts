using MyContacts.Data.Models;

namespace MyContacts.Data.DAL;

public interface IContactRepository : IDisposable
{
    IEnumerable<Contact> GetContacts();
    Contact? GetContactByID(int id);
    Contact? GetContactByName(string name);
    Contact? GetContactByPhoneNumber(string phoneNumber);
    void InsertContact(Contact contact);
    void InsertSubContact(Contact contact, Contact subContact);
    void RemoveSubContact(Contact contact, Contact subContact);
    void UpdateSubContact(Contact contact, Contact subContact);
    void UpdateContact(Contact contact);
    void DeleteContact(int contactID);
    void Save();
}