using System.Collections;
using MyContacts.Data.Models;

namespace MyContacts.Services;

public interface IContactService : IDisposable
{
    
    /**
     * 
     */
    IEnumerable<Contact> GetFamiliarContacts((Contact, Contact) contacts);
    void AddContactToContactUser(Contact user, Contact contact);
    void RemoveContactFromContactUser(Contact user, Contact contact);

    bool IsPhoneNumberFormatValid(string number);
    // CRUD operations
    Contact GetContactUser(int id);
    void CreateContactUser(Contact user);
    void DeleteContactUser(Contact user);
    void UpdateContactUser(Contact user, ContactDTO info);
}