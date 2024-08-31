using System.Collections;
using MyContacts.Data.Models;

namespace MyContacts.Services;

public interface IContactService : IDisposable
{
    IEnumerable<Contact> GetUserContacts(Contact user);
    /**
     * 
     */
    IEnumerable<Contact> GetFamiliarContacts((Contact, Contact) contacts);
    
    void ChangeUserContact(Contact user, int contactID);
    void UpdateUserContact(Contact user, Contact contact);
    void AddContactToUser(Contact user, Contact contact);
    
    
    // CRUD operations
    Contact GetUser(int ID);
    void CreateUser(Contact user);
    void DeleteUser(Contact user);
    void UpdateUser(Contact user, ContactDTO info);
}