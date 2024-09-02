using System.Collections;
using MyContacts.Data.Models;

namespace MyContacts.Services;

public interface IContactService : IDisposable
{
    
    /**
     * 
     */
    IEnumerable<Contact> GetFamiliarContacts((Contact, Contact) contacts);
    void AddSubContact(Contact user, Contact subContact);
    void RemoveSubContact(Contact user, Contact subContact);
    void EditSubContact(Contact user, Contact subContact);

    
    bool IsPhoneNumberFormatValid(string number);
    // CRUD operations
    Contact GetContactUser(int id);
    Contact GetContactUser(string phoneNumber);
    void CreateContactUser(Contact user);
    void DeleteContactUser(Contact user);
    void UpdateContactUser(Contact user);
}